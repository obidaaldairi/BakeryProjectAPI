using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; } = string.Empty;
        public string ArabicRoleName { get; set; } = string.Empty;
        [NotMapped]
        public ICollection<User> Users { get; set; } = new List<User>();
        [NotMapped]
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
