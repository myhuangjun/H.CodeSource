//// See https://aka.ms/new-console-template for more information

using ConsoleTestApp;
using H.EnterpriseWechatInterface;
using H.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

//Console.WriteLine("Hello, World!");
IServiceCollection services = new ServiceCollection();
services.AddLogging(logBuilder =>
{
    logBuilder.AddConsole();
    logBuilder.AddNLog();
    logBuilder.SetMinimumLevel(LogLevel.Trace);
});
services.AddScoped<TestService>();

//services.AddScoped<Logger>();
//services.AddEnterpriseWechat();
using (var sp = services.BuildServiceProvider())
{
    //var service = sp.GetService<IEnterpriseWechatInterface>();
    //Console.WriteLine(service.Get_Access_TokenAsync());
    var test = sp.GetRequiredService<TestService>();
    test.Test();
}
//Console.Read();

//Console.WriteLine("Hello, World!");