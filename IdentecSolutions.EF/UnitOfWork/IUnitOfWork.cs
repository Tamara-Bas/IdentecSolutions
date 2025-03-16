namespace IdentecSolutions.EF.UnitOfWork
{
    public interface IUnitOfWork
    {   
        ApplicationDbContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
