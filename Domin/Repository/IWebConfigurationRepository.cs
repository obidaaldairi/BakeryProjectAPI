using Domin.Entity;

namespace Domin.Repository
{
    public interface IWebConfigurationRepository : IGenericRepository<WebConfiguration>
    {
        string GetValueByKeyName(string key);
    }
}
