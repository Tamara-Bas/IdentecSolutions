namespace IdentecSolutions.Domain.Exceptions
{
    public class ApplicationException :Exception
    {
        protected ApplicationException()
        { }
        protected ApplicationException(string? message) : base(message)
        {
        }

        protected ApplicationException(string? message, Exception innerException) : base(message, innerException)
        {
        }
        protected ApplicationException(string? titile, string? message) : base(message) => Title = titile;
   
        public string Title { get; }
    }
}
