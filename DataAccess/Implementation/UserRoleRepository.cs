using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context) : base(context) { }

        public int Count()
        {
            return this.FindAllByCondition(q => q.IsDeleted == false).Count();
        }

        public string GetUserRole(Guid userID)
        {
            return this.FindByConditionWithIncludes(
                ur => ur.UserId == userID,
                u => u.Role)?.Role?.EnglishRoleName;
        }
    }





}
