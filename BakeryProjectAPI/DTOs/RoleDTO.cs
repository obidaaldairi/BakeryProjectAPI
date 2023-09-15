using System.ComponentModel.DataAnnotations;

namespace BakeryProjectAPI.DTOs
{
    public class RoleDTO
    {
        public string ArabicRoleName { get; set; }
        [Required]
        public string EnglishRoleName { get; set; }
    }
}
