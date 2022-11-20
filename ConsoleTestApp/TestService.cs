using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    public class TestService
    {
        private readonly ILogger<TestService> logger;
        public TestService(ILogger<TestService> logger)
        {
            this.logger = logger;
        }

        public void Test()
        {
            logger.LogDebug("开始计算");
            logger.LogDebug("结束计算");
            logger.LogError("计算报错");
        }
    }
}
