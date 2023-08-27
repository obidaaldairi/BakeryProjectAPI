using System.ComponentModel.DataAnnotations;

namespace BakeryProjectAPI.DTOs
{
    public class ProductImageDTO
    {
        [Required]
        public Guid ProductID { get; set; }
        public string Image { get; set; }
    }
}
