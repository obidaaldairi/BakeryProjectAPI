using System.ComponentModel.DataAnnotations;

namespace BakeryProjectAPI.DTOs
{
    public class RegisterDTO
    {
        public string ArabicUserName { get; set; }
        [Required]
        public string EnglishUserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and Confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid RoleID { get; set; }

    }
}
