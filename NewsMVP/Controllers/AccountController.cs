using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsMVP.Models;
using NewsMVP.Models.Dto;
using NewsMVP.MOdels;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsMVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly NewsContext _context;

        public AccountController(NewsContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
        {
            var user = await _context.TblUser.FirstOrDefaultAsync(u => u.UserName == dto.UserName && u.Password == dto.Password);

            if (user == null)
                return Unauthorized("نام کاربری یا رمز عبور اشتباه است.");


            var token = GenerateJWT("User", user.UserName, user.Id);
            return Ok(token);
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignUpDto dto)
        {
            if (await _context.TblUser.AnyAsync(u => u.UserName == dto.UserName))
                return BadRequest("نام کاربری تکراری است.");

            var user = dto.ToTbl();
            user.RoleId = 2;
            await _context.TblUser.AddAsync(user);
            await _context.SaveChangesAsync();
            var token = GenerateJWT("User", user.UserName, user.Id);
            return Ok(token);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("NewsDetail", new { id = dto.NewsId });

            dto.Profile = "Default.png";

            var comment = dto.ToTbl();
            comment.IsValid = true;
            await _context.TblComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("NewsDetail", new { id = dto.NewsId });
        }
        private string GenerateJWT(string RoleName , string Username , int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("137c514cc904eb0cc089aca19fdab93c68e859249a335331368c893818c64b91"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, RoleName),
                new Claim(ClaimTypes.Name, Username),
                new Claim("UserId", userId.ToString())
            };
            var Token = new JwtSecurityToken(
                issuer: "Developer",
                audience:"User",
                claims: claims,
                expires : DateTime.Now.AddMinutes(30),
                signingCredentials:creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token); 
            }
    }

}
