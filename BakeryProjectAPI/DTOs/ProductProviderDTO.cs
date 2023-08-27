using System.ComponentModel.DataAnnotations;

namespace BakeryProjectAPI.DTOs
{
    public class ProductProviderDTO
    {
        [Required]
        public Guid ProviderID { get; set; }
        [Required]
        public Guid ProductID { get; set; }
    }
}
