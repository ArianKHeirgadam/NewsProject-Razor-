using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVP.Models;
using NewsMVP.Models.Dto;
using NewsMVP.MOdels;
using NuGet.Protocol.Plugins;

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

            
            return Ok($"ورود موفقیت‌آمیز. خوش آمدید {user.Name}");
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

            return Ok("ثبت‌نام با موفقیت انجام شد.");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("NewsDetail", new { id = dto.NewsId });

            dto.Profile = "Default.png"; //  تضمین از سمت سرور

            var comment = dto.ToTbl();
            comment.IsValid = true;
            await _context.TblComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("NewsDetail", new { id = dto.NewsId });
        }


    }

}
