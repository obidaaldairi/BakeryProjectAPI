using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class ProductImages : BaseEntity
    {
        [ForeignKey("ProductID")]
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public string Image { get; set; }
    }
}
