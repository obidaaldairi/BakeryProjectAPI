using Domin.Entity;

namespace Domin.Repository
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        public int Count();
    }
}
