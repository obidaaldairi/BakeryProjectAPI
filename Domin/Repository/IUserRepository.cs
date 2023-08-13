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
        //public List<UserDTO> Search(string filter = "");

    }
}
