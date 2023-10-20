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
using Microsoft.EntityFrameworkCore;

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

        public async Task<UserInfoDTO> GetUserInfo(string userID)
        {
            Guid userId = Guid.Parse(userID);

            var userInfo = await (from user in _context.tblUsers
                        join userRoles in _context.tblUserRoles on user.ID equals userRoles.UserId
                        join role in _context.tblRoles on userRoles.RoleId equals role.ID
                        //join admin in _context.tblAdmins on users.ID equals admin.UserID
                        //join provider in _context.tblProviders on users.ID equals provider.UserID
                        where user.ID == userId && user.IsDeleted == false
                        select new UserInfoDTO
                        {
                            BirthDate = user.BirthDate,
                            EmailConfirmed = user.EmailConfirmed,
                            ArabicBio = user.ArabicBio,
                            ArabicUserName = user.ArabicUserName,
                            Avatar = user.Avatar,
                            Email = user.Email,
                            EnglishUserName = user.EnglishUserName,
                            PhoneNumber = user.PhoneNumber,
                            EnglishBio = user.EnglishBio,
                            RoleName = role.EnglishRoleName,

                            UserID= user.ID.ToString(),

                            AdminID = (role.EnglishRoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?
                            (from users in _context.tblUsers
                             join admin in _context.tblAdmins on users.ID equals admin.UserID
                             where users.ID == userId
                             select admin.ID.ToString())
                            .FirstOrDefault() : ""),

                            ProviderID = (role.EnglishRoleName.Equals("Provider", StringComparison.OrdinalIgnoreCase) ?
                            (from users in _context.tblUsers
                             join provider in _context.tblProviders on users.ID equals provider.UserID
                             where users.ID == userId
                             select provider.ID.ToString())
                            .FirstOrDefault() : ""),

                        }).FirstOrDefaultAsync();
            return userInfo;
        }


        //public async Task<UserInfoDTO> GetUserInfo(string userID)
        //{
        //    Guid userId = Guid.Parse(userID);
        //    var userInfoDTO =await  _context.tblUsers.Where(q => q.ID == userId)
        //        .Select(q => new UserInfoDTO
        //        {
        //            UserID = q.ID.ToString(),
        //            Avatar = q.Avatar,
        //            ArabicBio = q.ArabicBio,
        //            ArabicUserName = q.ArabicUserName,
        //            BirthDate = q.BirthDate,
        //            Email = q.Email,
        //            EmailConfirmed = q.EmailConfirmed,
        //            EnglishBio = q.EnglishBio,
        //            EnglishUserName = q.EnglishUserName,
        //            PhoneNumber = q.PhoneNumber

        //        }).FirstOrDefaultAsync();
        //    return userInfoDTO;
        //}

        public void UserSeeding()
        {
            if (!_context.tblUsers.Any())
            {

                // Add to the User table
                var users = _context.tblUsers.Add(new User
                {
                    EnglishUserName = "Admin",
                    ArabicBio = "",
                    ArabicUserName = "Admin",
                    BirthDate = new DateTime(),
                    CreatedAt = DateTime.Now,
                    Email = "Admin@Admin.com",
                    EnglishBio = "",
                    LastLoginDate = new DateTime(),
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin1234*"),
                    PhoneNumber = "0000000000",
                    PhoneNumberConfirmed = false,
                    EmailConfirmed = false,
                    IsActive = false,
                    IsDeleted = false,
                    Avatar = $"https://ui-avatars.com/api/?name=Admin&length=1"
                });
                _context.SaveChanges();


                var roleID = _context.tblRoles.Where(q => q.IsDeleted == false && q.EnglishRoleName.Contains("SuperAdmin"))
                    .Select(q => q.ID)
                    .FirstOrDefault();


                // Add to UserRoles table
                var role = _context.tblUserRoles.Add(new UserRole
                {
                    UserId = users.Entity.ID,
                    RoleId = roleID,
                    IsDeleted = false,
                });
                _context.SaveChanges();


                // Add to Admin table
                var admin = _context.tblAdmins.Add(new Admin
                {
                    IsDeleted = false,
                    UserID = users.Entity.ID,
                });
                _context.SaveChanges();
            }
        }
    }

}
