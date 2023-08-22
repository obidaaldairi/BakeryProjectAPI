using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class ProductProvider : BaseEntity
    {
        [ForeignKey("ProviderID")]
        public Guid ProviderID { get; set; }
        public Provider Provider { get; set; }
        [ForeignKey("ProductID")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
    }
}
