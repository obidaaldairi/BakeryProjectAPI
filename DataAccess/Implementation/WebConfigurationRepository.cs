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
    public  class WebConfigurationRepository : GenericRepository<WebConfiguration>,IWebConfigurationRepository
    {
        public WebConfigurationRepository(AppDbContext context) : base(context) { }

        public string GetValueByKeyName(string key)
        {
            var value = this.FindByCondition(q => q.ConfigKey.Equals(key, StringComparison.OrdinalIgnoreCase)).ConfigValue;
            return (!string.IsNullOrEmpty(value) ? value : "");
        }
    }
}
