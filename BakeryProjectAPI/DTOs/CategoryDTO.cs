using System.ComponentModel.DataAnnotations;

namespace BakeryProjectAPI.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public string ArabicTitle { get; set; }
        [Required]
        public string EnglishTitle { get; set; }
    }
}
