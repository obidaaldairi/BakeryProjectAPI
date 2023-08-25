using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
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
