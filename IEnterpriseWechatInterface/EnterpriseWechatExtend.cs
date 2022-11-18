using H.EnterpriseWechatInterface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 企业微信拓展
    /// </summary>
    public static class EnterpriseWechatExtend
    {
        public static IServiceCollection AddEnterpriseWechat(this IServiceCollection services)
        {
            services.AddScoped<IEnterpriseWechatInterface,EnterpriseWechatService>();
            return services;
        }
    }
}
