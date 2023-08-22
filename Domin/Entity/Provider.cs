using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class Provider : BaseEntity
    {
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public User User { get; set; }
        public List<ProductProvider> ProductProviders { get; set; }
    }
}
