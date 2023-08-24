using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class Product : BaseEntity
    {
        public string  ProductName { get; set; }
        public string  Description { get; set; }
        public double  Price { get; set; }

        [ForeignKey("CategoryID")]
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }

        [ForeignKey("SubCategoryID")]
        public Guid SubCategoryID { get; set; }
        public SubCategory SubCategory { get; set; }
        [NotMapped]
        public List<ProductImages> ProductImages { get; set; }
        [NotMapped]
        public List<ProductProvider> ProductProviders { get; set; }
    }
}
