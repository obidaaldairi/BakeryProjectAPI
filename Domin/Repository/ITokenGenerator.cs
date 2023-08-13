using Domin.Entity;

namespace Domin.Repository
{
    public interface ITokenGenerator
    {
        public string CreateToken(User user);

    }
}
