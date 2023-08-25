using Domin.Entity;

namespace Domin.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public List<Category> Search(string filter = "");
        public int Count();
    }
}
