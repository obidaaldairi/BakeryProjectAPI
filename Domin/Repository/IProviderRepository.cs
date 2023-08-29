using Domin.Entity;

namespace Domin.Repository
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        public Guid GetCurrentLoggedInUserID();
        public Guid GetCurrentLoggedInUserEmail();
        public Guid GetCurrentLoggedInUserRole();

    }
}
