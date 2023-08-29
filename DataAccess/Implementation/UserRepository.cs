using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace DataAccess.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Count()
        {
            return this.FindAllByCondition(q=>q.IsDeleted==false).Count();
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

        public List<User> Search(string filter = "")
        {
            filter = filter.ToLower();
            if (string.IsNullOrEmpty(filter))
            {
                return this.FindAllByCondition(q=>q.IsDeleted==false);
            }
            else
            {
                return this.FindAllByCondition(q => q.IsDeleted == false && (q.ArabicUserName.Contains(filter)
                ||q.EnglishUserName.Contains(filter)
                || q.Email.Contains(filter)));
            }
        }
    }

}
