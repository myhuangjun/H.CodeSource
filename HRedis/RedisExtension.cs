using HRedis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisExtension
    {
        public static IServiceCollection AddHRedis(this IServiceCollection services,Action<RedisSetting> setting)
        {
            services.Configure<RedisSetting>(setting);
            services.AddScoped<RedisHelper>();
            return services;
        }
    }


    public class RedisSetting
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 默认前缀
        /// </summary>
        public string DefaultKey { get; set; }
        /// <summary>
        /// 默认数据库
        /// </summary>
        public int Db { get; set; } = -1;
    }
}
