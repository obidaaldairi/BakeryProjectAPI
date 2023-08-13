using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        // Photo
        public string Avatar { get; set; } = string.Empty;
        [NotMapped]
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        [NotMapped]
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
