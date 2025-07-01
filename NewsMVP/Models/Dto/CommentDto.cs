using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NewsMVP.MOdels;

namespace NewsMVP.Models.Dto
{
    public class CommentDto
    {
        public string Author { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateOnly Date { get; set; }
        public int NewsId { get; set; }
        public bool IsValid { get; set; }
        public string Profile { get; set; } = null!;

        public TblComments ToTbl()
        {
            return new TblComments
            {
                Author = Author,
                Body = Body,
                Date = Date,
                NewsId = NewsId,
                IsValid = IsValid,
                Profile = Profile
            };
        }
    }

}
