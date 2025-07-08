using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVP.Models;
using NewsMVP.Models.Dto;
using NewsMVP.MOdels;



 
 

namespace NewsMVP.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsContext _Context;

        public HomeController(NewsContext context)
        {
            _Context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> ShowAll(int page = 1, string category = null, string search = null)
        {
            int pageSize = 6;
            var query = _Context.TblNews.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(n => n.CategoryName == category);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(n =>
                    n.Title.Contains(search) ||
                    n.CategoryName.Contains(search));
            }

            var newsList = await query
                .OrderByDescending(n => n.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int totalItems = await query.CountAsync();

            var comments = await _Context.TblComments.ToListAsync();

            var model = new HomeViewModel
            {
                AkharinNews = newsList,
                Comments = comments,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                SelectedCategory = category
            };

            return View(model);
        }

        public async Task<IActionResult> NewsDetail(int id)
        {
            var news = await _Context.TblNews.FirstOrDefaultAsync(n => n.Id == id);
            if (news == null) return NotFound();

            var comments = await _Context.TblComments
                .Where(c => c.NewsId == id && c.IsValid)
                .OrderByDescending(c => c.Date)
                .ToListAsync();

            var vm = new NewsDetailViewModel
            {
                News = news,
                Comments = comments
            };

            return View(vm);
        }

        public IActionResult ContactUs() => View();
        public IActionResult Login() => View();
        public IActionResult Signup() => View();
        public IActionResult Eror404() => View();

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("NewsDetail", new { id = dto.NewsId });

            dto.Profile = "Default.png";
            var comment = dto.ToTbl();
            comment.IsValid = true;

            await _Context.TblComments.AddAsync(comment);
            await _Context.SaveChangesAsync();

            return RedirectToAction("NewsDetail", new { id = dto.NewsId });
        }
    }

}
