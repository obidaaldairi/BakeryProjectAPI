namespace BakeryProjectAPI.DTOs
{
    public class LoginDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserID { get; set; }
        public string UserImage { get; set; } = string.Empty;

    }
}
