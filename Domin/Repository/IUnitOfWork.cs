namespace Domin.Repository
{
    public interface IUnitOfWork : IDisposable 
    {
        IUserRepository User { get; }
        IUserVerificationRepository UserVerification { get; }
        IRoleRepository Role { get; }
        IWebConfigurationRepository WebConfiguration { get; }
        void Commit();
    }

}
