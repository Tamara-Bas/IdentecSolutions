using IdentecSolutions.Domain.Entities;

namespace IdentecSolutions.EF.Repository
{
    interface IBaseRepository<TEntity> where TEntity : Entity
    {

        IQueryable<TEntity> Queryable();
        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        void Delete(int id);

        Task DeleteAsync(int id);

        
    }
}
