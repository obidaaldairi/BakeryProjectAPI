using Domin.Entity;

namespace Domin.Repository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        public void RoleSeeding();
        public Guid GetRoleIdByName(string roleName);
    }
}
