using System.ComponentModel.DataAnnotations.Schema;

namespace Domin.Entity
{
    public class UserInformation : BaseEntity
    {
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public User User { get; set; }

        public string  City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        //public Gender Gender { get; set; }
        //public Country Country { get; set; }
    }
}
