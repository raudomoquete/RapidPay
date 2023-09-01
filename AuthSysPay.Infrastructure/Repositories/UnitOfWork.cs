using AuthSysPay.Core;

namespace AuthSysPay.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthSysPayContext _context;
        private readonly IRepository<Card> _cardRepository;

        public UnitOfWork(AuthSysPayContext context)
        {
            _context = context;
        }

        public IRepository<Card> CardRepository => _cardRepository ?? new BaseRepository<Card>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }

        public async Task SaveChagesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
