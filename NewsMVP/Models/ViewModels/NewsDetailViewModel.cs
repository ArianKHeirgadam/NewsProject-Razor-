using NewsMVP.MOdels;

namespace NewsMVP.Models
{
    public class NewsDetailViewModel
    {
        public TblNews News { get; set; }
        public List<TblComments> Comments { get; set; }
        public List<TblNews> NewsList { get; set; }
        public List<TblCategories> Categories { get; set; }
        public List<TblUser> Users { get; set; }
        public List<TblKeyword> Keyword { get; set; }
    }
}
