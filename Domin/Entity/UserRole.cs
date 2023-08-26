﻿namespace Domin.Entity
{
    public class UserRole:BaseEntity
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
