using H.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
