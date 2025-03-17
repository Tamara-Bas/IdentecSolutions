using System.Net.NetworkInformation;

namespace IdentecSolutions.WebApi.Extensions
{
    public static partial  class BuilderExtensions
    {
        public static IApplicationBuilder UseExceptionMiddelware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlingMiddleware>();
            return builder;
        }
    }
}
