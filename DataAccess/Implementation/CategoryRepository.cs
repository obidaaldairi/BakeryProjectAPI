using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context) { _context = context; }

        public void CategorySeeding()
        {
            var checkData = _context.tblCategories.Any();
            if (!checkData) 
            {
            
                List<Category> categories = new List<Category>()
                {
                    new Category(){ArabicTitle="Electronics",EnglishTitle="Electronics",IsDeleted=false},
                    new Category(){ArabicTitle="Fashion",EnglishTitle="Fashion",IsDeleted=false},
                    new Category(){ArabicTitle="Sports",EnglishTitle="Sports",IsDeleted=false},
                    new Category(){ArabicTitle="Arts",EnglishTitle="Arts",IsDeleted=false}
                };
                _context.tblCategories.AddRange(categories);
                _context.SaveChanges();
            }
        }

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
