using DataAccess.Context;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork()
        {

        }

        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
            User = new UserRepository();
            Role = new RoleRepository();
        }

        public IUserRepository User  { get; private set; }

        public IRoleRepository Role { get; private set; }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
