namespace Domin.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IUserVerificationRepository UserVerification { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRole { get; }
        IWebConfigurationRepository WebConfiguration { get; }
        ICypherServices CypherServices { get; }
        IDbInitializer DbInitializer { get; }
        ICategoryRepository Category { get; }
        void Commit();
    }

}
