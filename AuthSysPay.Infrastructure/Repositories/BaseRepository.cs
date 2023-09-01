using AuthSysPay.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthSysPay.Infrastructure
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AuthSysPayContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(AuthSysPayContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FirstAsync(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
        }
    }
}
