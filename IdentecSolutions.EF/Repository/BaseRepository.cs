using IdentecSolutions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefaultAsync(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Single(predicate);
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleAsync(predicate);
        }
        
        public TEntity Update(TEntity entity)
        {
            var catalogue = _entities.Find(entity.Id);
            SetEntryModified(entity);
            _context.Entry(catalogue).CurrentValues.SetValues(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var catalogue = await _entities.FindAsync(entity.Id);
            SetEntryModified(entity);
            _context.Entry(catalogue).CurrentValues.SetValues(entity);
            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public List<TEntity> GetAllList()
        {
            return _entities.ToList();
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).ToListAsync();
        }

        public Task<List<TEntity>> GetAllListAsync()
        {
            return _entities.ToListAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.CountAsync(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Any(predicate);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AnyAsync(predicate);
        }

        public bool Contains(TEntity entity)
        {
            return _entities.Contains(entity);
        }

        private void SetEntryModified(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
