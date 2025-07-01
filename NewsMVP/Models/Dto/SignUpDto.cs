using NewsMVP.MOdels;

namespace NewsMVP.Models.Dto
{
    public class SignUpDto
    {
        public string Name { get; set; } = null!;
        public string Tell { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }

        public TblUser ToTbl()
        {
            return new TblUser
            {
                Name = Name,
                Tell = Tell,
                UserName = UserName,
                Password = Password, 
                RoleId = RoleId
            };
        }
    }
}
