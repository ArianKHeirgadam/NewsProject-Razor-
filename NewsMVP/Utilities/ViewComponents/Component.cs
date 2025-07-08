using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVP.Models;
using NewsMVP.Models.ViewModels;
using NewsMVP.MOdels;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

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
            var news = await _context.TblNews
                .Where(n => n.CategoryName == categoryName)
                .OrderByDescending(n => n.Date)
                .Take(5)
                .ToListAsync();

            return View("Default", news);
        }
    }


    public class LatestNewsHome : ViewComponent
    {
        private readonly NewsContext _context;

        public LatestNewsHome(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var newsList = await _context.TblNews
     .OrderByDescending(n => n.Date)
     .Take(3)
     .ToListAsync();

            var result = new List<NewsWithCommentCount>();

            foreach (var news in newsList)
            {
                int count = await _context.TblComments.CountAsync(c => c.NewsId == news.Id && c.IsValid);
                result.Add(new NewsWithCommentCount
                {
                    News = news,
                    CommentCount = count
                });
            }

            return View(result);
        }
    }

 
    public class HotNewsViewComponent : ViewComponent
    {
        private readonly NewsContext _context;

        public HotNewsViewComponent(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var hotNews = await _context.TblNews.OrderByDescending(n => n.ViewCount).Take(3).ToListAsync();
            return View(hotNews);
        }
    }


    public class KeywordsViewComponent : ViewComponent
    {
        private readonly NewsContext _context;

        public KeywordsViewComponent(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var keywords = await _context.TblKeyword.ToListAsync();
            return View(keywords);
        }
    }

 
    public class CommentFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int newsId)
        {
            return View(newsId);
        }
    }
    public class CommentListViewComponent : ViewComponent
    {
        private readonly NewsContext _context;

        public CommentListViewComponent(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int newsId)
        {
            var comments = await _context.TblComments
                .Where(c => c.NewsId == newsId && c.IsValid)
                .OrderByDescending(c => c.Date)
                .ToListAsync();

            return View(comments);
        }

    }
    public class NewsGridViewComponent : ViewComponent
    {
        private readonly NewsContext _context;

        public NewsGridViewComponent(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<TblNews> newsList)
        {
            var result = new List<NewsWithCommentCount>();

            foreach (var news in newsList)
            {
                int count = await _context.TblComments
                    .CountAsync(c => c.NewsId == news.Id && c.IsValid);

                result.Add(new NewsWithCommentCount
                {
                    News = news,
                    CommentCount = count
                });
            }

            return View(result);
        }
    }

    public class NewsWithCommentCount
    {
        public TblNews News { get; set; }
        public int CommentCount { get; set; }
    }
    public class MainSliderViewComponent : ViewComponent
    {
        private readonly NewsContext _context;

        public MainSliderViewComponent(NewsContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var slides = await _context.TblNews.OrderByDescending(n => n.Date).Take(3).ToListAsync();
            return View(slides);
        }
    }
    public class SideNewsViewComponent : ViewComponent
{
    private readonly NewsContext _context;

    public SideNewsViewComponent(NewsContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var sideNews = await _context.TblNews
            .OrderByDescending(n => n.ViewCount)
            .Take(2)
            .ToListAsync();

        return View(sideNews);
    }
}

    public class NewsArticleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(TblNews news, List<TblComments> comments)
        {
            var vm = new NewsArticleViewModel
            {
                News = news,
                Comments = comments
            };

            return View(vm);
        }
    }

    public class NewsArticleViewModel
    {
        public TblNews News { get; set; }
        public List<TblComments> Comments { get; set; }
    }

}
