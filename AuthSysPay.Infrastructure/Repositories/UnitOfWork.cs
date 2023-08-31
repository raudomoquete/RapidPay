using AuthSysPay.Core;

namespace AuthSysPay.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthSysPayContext _context;

        public UnitOfWork(AuthSysPayContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
