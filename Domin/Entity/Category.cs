using System.ComponentModel.DataAnnotations;

namespace Domin.Entity
{
    public class Category : BaseEntity
    {
        [Required]
        public string Title { get; set; }
    }
}
