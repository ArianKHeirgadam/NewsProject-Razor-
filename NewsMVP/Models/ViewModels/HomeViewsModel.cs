

using NewsMVP.MOdels;

namespace NewsMVP.Models
{
    public class HomeViewModel
    {
        public List<TblNews> Slider { get; set; }
        public List<TblNews> Latest { get; set; }
        public List<TblNews> Side {  get; set; }
        public List<TblNews> AkharinNews{ get; set; }
        public List<TblComments> Comments { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public string? SelectedCategory { get; set; }
        public Dictionary<string, List<TblNews>> CategorizedNews { get; set; }


    }

}