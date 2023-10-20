using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDTOs.DTOs
{
    public class ProductDTO
    {
        public string EnglishProductName { get; set; }
        public string ArabicProductName { get; set; }
        public string EnglishDescription { get; set; }
        public string ArabicDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryID { get; set; }
        public Guid ProviderID { get; set; }
        public Guid UserID { get; set; }
    }
}
