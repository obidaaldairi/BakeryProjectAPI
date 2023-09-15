using Domin.Entity;

namespace Domin.Repository
{
    public interface IUserVerificationRepository : IGenericRepository<UserVerification>
    {
        UserVerification VerfiyUserVerficationCode(string UserID, string VerficationCode);
    }
}
