using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVP.Models;
using NewsMVP.MOdels;
using System.Linq;
using System.Threading.Tasks;

namespace NewsMVP.Utilities.ViewComponents
{
    public class MenuCategories : ViewComponent
    {
        private readonly NewsContext _context;

        public MenuCategories(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.TblCategories.ToListAsync();
            return View(categories);
        }
    }

    public class TrendingNews : ViewComponent
    {
        private readonly NewsContext _context;

        public TrendingNews(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var trending = await _context.TblNews.OrderByDescending(n => n.Date).Take(4).ToListAsync();

            return View(trending);
        }
    }

    public class LatestNewsFooter : ViewComponent
    {
        private readonly NewsContext _context;

        public LatestNewsFooter(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var latestNews = await _context.TblNews.OrderByDescending(n => n.Date).Take(2).ToListAsync();

            return View(latestNews);
        }
    }

    public class SocialLinks : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

    public class SideNewsMobile : ViewComponent
    {
        private readonly NewsContext _context;
        public SideNewsMobile(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var latestNews = await _context.TblNews.OrderByDescending(n => n.Date).FirstOrDefaultAsync();


            if (latestNews == null)
            {
                latestNews = new TblNews { Title = "تست ViewComponent", ImageUrlno1 = "test.jpg" };
            }

            return View(latestNews);

        }
    }
    public class CategoryNewsViewComponent : ViewComponent
    {
        private readonly NewsContext _context;

        public CategoryNewsViewComponent(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryName)
        {
            var news = await _context.TblNews.Where(n => n.CategoryName == categoryName).OrderByDescending(n => n.Date).Take(5).ToListAsync();

            return View("Default", news);
        }
    }
    public class LatestNewsHome : ViewComponent
    {
        private readonly NewsContext _context;
        public LatestNewsHome(NewsContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newsWithCommentCounts = await _context.TblNews
                .OrderByDescending(n => n.Date)
                .Take(3)
                .Select(n => new
                {
                    News = n,
                    CommentCount = _context.TblComments.Count(c => c.NewsId == n.Id && c.IsValid)
                })
                .ToListAsync();

            return View(newsWithCommentCounts);
        }
    }
}
    




