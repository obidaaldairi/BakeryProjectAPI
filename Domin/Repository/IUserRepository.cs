using Domin.DTOS;
using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public List<User> Search(string filter = "");
        public int Count();
        public Guid GetCurrentLoggedInUserID();
        public string GetCurrentLoggedInUserEmail();
        public string GetCurrentLoggedInUserRole();
        public UserInfoDTO GetUserInfo(string userID);
    }
}
