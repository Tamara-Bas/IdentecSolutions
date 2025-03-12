using IdentecSolutions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentecSolutions.EF.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
       where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<TEntity> _entities;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        public IQueryable<TEntity> Queryable()
        {
            return _entities;
        }

        public virtual TEntity Add(TEntity entity)
        {
            var savedEntity = _entities.Add(entity).Entity;
            return savedEntity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var savedEntity = (await _entities.AddAsync(entity)).Entity;
            return savedEntity;
        }

        public void Delete(int id)
        {
            var catalogue = _entities.Find(id);
            _entities.Remove(catalogue);
        }

        public async Task DeleteAsync(int id)
        {
            var catalogue = await _entities.FindAsync(id);
            _entities.Remove(catalogue);
        }

        public async void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

       
    }
}
