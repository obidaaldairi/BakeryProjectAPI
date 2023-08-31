using Domin.Entity;

namespace Domin.Repository
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        public Guid GetCurrentLoggedInUserID();
        public string GetCurrentLoggedInUserEmail();
        public string GetCurrentLoggedInUserRole();

    }
}
