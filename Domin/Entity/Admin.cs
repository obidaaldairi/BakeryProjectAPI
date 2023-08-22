using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class Admin : BaseEntity
    {
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
