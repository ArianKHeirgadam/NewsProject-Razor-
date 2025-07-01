using NewsMVP.MOdels;

namespace NewsMVP.Models.ViewModels
{
    public class NewsWithCommentCount
    {
        public TblNews News { get; set; }
        public int CommentCount { get; set; }
    }
}
