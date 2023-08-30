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
        public string  EnglishProductName { get; set; }
        public string  ArabicProductName { get; set; }
        public string  EnglishDescription { get; set; }
        public string  ArabicDescription { get; set; }
        public decimal  Price { get; set; }
        public int Quantity { get; set; }


        [ForeignKey("CategoryID")]
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public List<ProductImages> ProductImages { get; set; }
        [NotMapped]
        public List<ProductProvider> ProductProviders { get; set; }
    }
}
