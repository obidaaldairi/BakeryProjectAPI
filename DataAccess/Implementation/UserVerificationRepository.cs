using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class UserVerificationRepository : GenericRepository<UserVerification>, IUserVerificationRepository
    {
        private readonly AppDbContext _context;
        public UserVerificationRepository(AppDbContext context) : base(context) { _context = context; }


        public UserVerification VerfiyUserVerficationCode(string UserID, string VerficationCode)
        {
            var verficationcode = _context.tblUserVerification.Where(
                q=>q.IsVerify==false
                && q.UserID.ToString()== UserID
                && q.ExpireDate >= DateTime.Now
                && q.CreationDate <= DateTime.Now
                && q.VerificationCode ==VerficationCode
                ).OrderByDescending( q => q.CreationDate ).FirstOrDefault();
                return verficationcode;
        }
    }



}
