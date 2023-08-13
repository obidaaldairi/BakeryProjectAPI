namespace Domin.Entity
{
    public class UserRole
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public Guid UsersId { get; set; }
        public User User { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
