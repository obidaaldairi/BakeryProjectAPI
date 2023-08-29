using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Implementation
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentLoggedInUserEmail()
        {
            var userEmailClaim = _httpContextAccessor.HttpContext.User.FindFirst("Email");

            if (userEmailClaim != null && Guid.TryParse(userEmailClaim.Value, out Guid userId))
            {
                return userId;
            }
            throw new InvalidOperationException("No logged-in user found.");
        }

        public Guid GetCurrentLoggedInUserID()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            throw new InvalidOperationException("No logged-in user found.");
        }

        public Guid GetCurrentLoggedInUserRole()
        {
            var userRoleClaim = _httpContextAccessor.HttpContext.User.FindFirst("Role");

            if (userRoleClaim != null && Guid.TryParse(userRoleClaim.Value, out Guid userId))
            {
                return userId;
            }
            throw new InvalidOperationException("No logged-in user found.");
        }

    }    

}
