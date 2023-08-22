using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class UserVerification : BaseEntity
    {
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public User User { get; set; }
        public string VerificationCode { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsVerify { get; set; }

    }
}
