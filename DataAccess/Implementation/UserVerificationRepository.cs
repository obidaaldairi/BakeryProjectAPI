using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class UserVerificationRepository : GenericRepository<UserVerification>, IUserVerificationRepository
    {
        public UserVerificationRepository(AppDbContext context) : base(context) { }



    }



}
