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

    }
}
