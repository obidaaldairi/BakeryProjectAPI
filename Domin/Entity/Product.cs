using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public List<ProductImages> ProductImages { get; set; }
        public List<ProductProvider> ProductProviders { get; set; }
    }

}
