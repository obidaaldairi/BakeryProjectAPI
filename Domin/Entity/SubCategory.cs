using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class SubCategory : BaseEntity
    {
        [Required]
        public string  ArabicTitle { get; set; }

        [Required]
        public string EnglishTitle { get; set; }
        [ForeignKey("CategoryID")]
        public Guid CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
