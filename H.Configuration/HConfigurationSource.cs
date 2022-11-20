using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HConfiguration
{
    /// <summary>
    /// 提供参数
    /// </summary>
    public class HConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);//处理Path等默认值的问题
            return new HConfigurationProvider(this);
        }
    }
}
