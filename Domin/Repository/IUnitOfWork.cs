namespace Domin.Repository
{
    public interface IUnitOfWork : IDisposable 
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        void Commit();
    }

}
