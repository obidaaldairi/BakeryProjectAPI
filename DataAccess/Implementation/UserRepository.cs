using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public int Count()
        {
            return this.FindAllByCondition(q=>q.IsDeleted==false).Count();
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

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public int Count()
        {
            return this.FindAllByCondition(q => q.IsDeleted == false).Count();
        }

        public List<Category> Search(string filter = "")
        {
            filter = filter.ToLower();
            if (string.IsNullOrEmpty(filter))
            {
                return this.FindAllByCondition(q => q.IsDeleted == false);
            }
            else
            {
                return this.FindAllByCondition(q => q.IsDeleted == false && (q.ArabicTitle.Contains(filter)
                || q.EnglishTitle.Contains(filter)));
            }
        }


    }




}
