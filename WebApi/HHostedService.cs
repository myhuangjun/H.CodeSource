namespace WebApi
{
    public class HHostedService : BackgroundService
    {
        public IServiceScope serviceScope;

        public HHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScope = serviceScopeFactory.CreateScope();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("HHostedService Start");
            var service=serviceScope.ServiceProvider.GetService<TestService>();
            await Task.Delay(3000);
            Console.WriteLine(service.Add(5,5));
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"第{i}次执行");
            }
            await Task.Delay(3000);
            Console.WriteLine("HHostedService执行完成");
        }
    }

    /// <summary>
    /// TestService为范围的服务
    /// </summary>
    public class TestService
    {
        public int Add(int i, int j) => i + j;
    }
}
