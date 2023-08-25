using System.ComponentModel.DataAnnotations;

namespace BakeryProjectAPI.DTOs
{
    public class RoleDTO
    {
        [Required]
        public string ArabicRoleName { get; set; }
        [Required]
        public string EnglishRoleName { get; set; }
    }
}
