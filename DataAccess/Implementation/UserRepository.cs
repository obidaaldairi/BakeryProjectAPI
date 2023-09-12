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
using Domin.DTOS;

namespace DataAccess.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public List<User> Search(string filter = "")
        {
            filter = filter.ToLower();
            if (string.IsNullOrEmpty(filter))
            {
                return this.FindAllByCondition(q => q.IsDeleted == false);
            }
            else
            {
                return this.FindAllByCondition(q => q.IsDeleted == false && (q.ArabicUserName.Contains(filter)
                || q.EnglishUserName.Contains(filter)
                || q.Email.Contains(filter)));
            }
        }

        public int Count()
        {
            return this.FindAllByCondition(q => q.IsDeleted == false).Count();
        }

        public string GetCurrentLoggedInUserEmail()
        {
            var userEmailClaim = _httpContextAccessor.HttpContext.User.FindFirst("Email");

            if (userEmailClaim is not null)
            {
                return userEmailClaim.Value;
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

        public string GetCurrentLoggedInUserRole()
        {
            var userRoleClaim = _httpContextAccessor.HttpContext.User.FindFirst("Role");
            if (userRoleClaim is not null)
            {
                return userRoleClaim.Value;
            }
            throw new InvalidOperationException("No logged-in user found.");
        }

        public UserInfoDTO GetUserInfo(string userID)
        {
            Guid userId = Guid.Parse(userID);
            var userInfoDTO = _context.tblUsers.Where(q => q.ID == userId)
                .Select(q => new UserInfoDTO
                {
                    UserID = q.ID.ToString(),
                    Avatar = q.Avatar,
                    ArabicBio = q.ArabicBio,
                    ArabicUserName = q.ArabicUserName,
                    BirthDate = q.BirthDate,
                    Email = q.Email,
                    EmailConfirmed = q.EmailConfirmed,
                    EnglishBio = q.EnglishBio,
                    EnglishUserName = q.EnglishUserName,
                    PhoneNumber = q.PhoneNumber

                }).FirstOrDefault();
            return userInfoDTO;
        }
    }

}
