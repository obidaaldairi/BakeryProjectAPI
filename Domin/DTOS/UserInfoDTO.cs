using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.DTOS
{
    public  class UserInfoDTO
    {
        public string  UserID { get; set; }
        public string EnglishUserName { get; set; }
        public string ArabicUserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Avatar { get; set; }
        public string ArabicBio { get; set; }
        public string EnglishBio { get; set; }
        public DateTime BirthDate { get; set; }
        public string  ProviderID { get; set; }
        public string AdminID { get; set; }
        public string  RoleName { get; set; }
    }
}
