namespace Domin.Repository
{
    public interface IUnitOfWork : IDisposable 
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        ITokenGenerator TokenGeneratorRepository { get; }

        void Save();
    }

}
