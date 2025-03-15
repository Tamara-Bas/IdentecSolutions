using IdentecSolutions.Domain.Entities;
using System.Linq.Expressions;

namespace IdentecSolutions.EF.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {

        IQueryable<TEntity> Queryable();

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        void Delete(int id);

        Task DeleteAsync(int id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        void Delete(TEntity entity);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetEnumerable(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAllList();

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        bool Contains(TEntity entity);

    }
}
