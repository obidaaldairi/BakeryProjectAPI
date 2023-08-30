using Domin.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryProjectAPI.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string EnglishProductName { get; set; }
        [Required]
        public string ArabicProductName { get; set; }
        public string EnglishDescription { get; set; }
        public string ArabicDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Guid CategoryID { get; set; }
    }
}
