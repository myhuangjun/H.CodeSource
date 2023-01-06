////// See https://aka.ms/new-console-template for more information

#region 通用命名空间
using ConsoleTestApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Net.Http.Headers;

IServiceCollection services = new ServiceCollection();
#endregion

#region 日志
//IServiceCollection services = new ServiceCollection();
//services.AddLogging(logBuilder =>
//{
//    logBuilder.AddConsole();
//    logBuilder.AddNLog();
//    logBuilder.SetMinimumLevel(LogLevel.Trace);
//});
//services.AddScoped<TestService>();
#endregion
#region Options
//services.AddEnterpriseWechat();
//services.AddEnterpriseWechat(set => {
//    set.Wechat_Secret = "****Secret";
//    set.Wechat_Corp_Id = "*****Corp_Id*******";
//    set.Wechat_AgentId = "********AgentId********";
//});
//using (var sp = services.BuildServiceProvider())
//{
//    var service = sp.GetRequiredService<IEnterpriseWechatInterface>();
//    Console.WriteLine(service.Get_Access_TokenAsync());
//}
#endregion
#region 表达式树
//表达式树
////using HExpression;
////var ex = new HExpressionExtension();
////Console.WriteLine(ex.GetExpressionString()); 

#endregion
#region 配置
//ConfigurationBuilder configBuilder = new ConfigurationBuilder();
//configBuilder.AddJsonFile("app.json",optional:true,true);//optional-false:如果文件不存在则报错
//IConfigurationRoot configurationRoot = configBuilder.Build();
//string name = configurationRoot["name"];
//string studentName = configurationRoot.GetSection("Student:Name").Value;
//var student = configurationRoot.GetSection("Student").Get<Student>();
//Console.WriteLine(student.Name+","+student.Id);
#endregion
Console.WriteLine("Hello");

#region Redis
//services.AddHRedis(opt =>
//{
//    opt.ConnectionString = "************";
//    opt.DefaultKey = "Test";
//    opt.Db = 10;
//});
//using var sp = services.BuildServiceProvider();
//var redis = sp.GetRequiredService<RedisHelper>();
//设置string
//var f = redis.StringSet("01", "测试01");
//Console.WriteLine(f?"设置成功":"设置失败");
//读取string,不存在则创建
//var value = redis.StringGet("02", null, () =>
//{
//    return new {Name="测试02"};
//});
//Console.WriteLine(value);
//设置Hash
//var dic = new Dictionary<string, string>
//{
//    { "01", "{\"Name\":\"张三\"}" },
//    { "02", "{\"Name\":\"李四\"}" }
//};
//var f= redis.SetHash("Student", dic);
//Console.WriteLine(f?"设置成功":"设置失败");
//读取Hash,不存在则创建
//var student = redis.GetHash("Student", "03", () =>
//{
//    var value = "{\"Name\":\"王五\"}";
//    return value;
//});
#endregion

