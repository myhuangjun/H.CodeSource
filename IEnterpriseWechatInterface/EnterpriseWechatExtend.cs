using HEnterpriseWechatInterface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
        public static IServiceCollection AddEnterpriseWechat(this IServiceCollection services,Action<WechatSetting> setting)
        {
            services.Configure<WechatSetting>(setting);
            services.AddScoped<IEnterpriseWechatInterface,EnterpriseWechatService>();
            return services;
        }
    }



    public class WechatSetting
    {
        /// <summary>
        /// CorpId
        /// </summary>
        public string Wechat_Corp_Id { get; set; }
        /// <summary>
        /// 微信Secret
        /// </summary>
        public string Wechat_Secret { get; set; }
        /// <summary>
        /// 应用Id
        /// </summary>
        public string Wechat_AgentId { get; set; }
    }
}
