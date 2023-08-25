using System.ComponentModel.DataAnnotations;

namespace BakeryProjectAPI.DTOs
{
    public class CategoryDTO
    {
        public string ArabicTitle { get; set; }
        public string EnglishTitle { get; set; }
        public Guid CategoryID { get; set; }

    }
}
