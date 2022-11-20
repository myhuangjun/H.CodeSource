using HLoggers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggerExtension
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
        {
            services.AddSingleton(typeof(Logger));
            return services; 
        }
    }
}
