using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class Cart : BaseEntity
    {
        [ForeignKey("ProductID")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }

        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public User User { get; set; }

        public int Count { get; set; }
    }
}
