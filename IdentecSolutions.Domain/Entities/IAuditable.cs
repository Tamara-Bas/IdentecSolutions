namespace IdentecSolutions.Domain.Entities
{
    public interface IAuditable<T>
    {
        public T AuditRecord { get; set; }
    }
}
