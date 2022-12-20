////// See https://aka.ms/new-console-template for more information

//#region 日志
///*
//using ConsoleTestApp;
//using H.EnterpriseWechatInterface;
//using H.Logger;
//using Microsoft.Extensions.Configuration;
using HEnterpriseWechatInterface;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using Microsoft.Extensions.Logging;
//using NLog.Extensions.Logging;
//using Microsoft.Extensions.Logging.Console;
IServiceCollection services = new ServiceCollection();
//services.AddLogging(logBuilder =>
//{
//    logBuilder.AddConsole();
//    logBuilder.AddNLog();
//    logBuilder.SetMinimumLevel(LogLevel.Trace);
//});
//services.AddScoped<TestService>();

////Console.WriteLine("Hello, World!");


//services.AddScoped<Logger>();
//services.AddEnterpriseWechat();
services.AddEnterpriseWechat(set => {
    set.Wechat_Secret = "****Secret";
    set.Wechat_Corp_Id = "*****Corp_Id*******";
    set.Wechat_AgentId = "********AgentId********";
});
using (var sp = services.BuildServiceProvider())
{
    var service = sp.GetRequiredService<IEnterpriseWechatInterface>();
    Console.WriteLine(service.Get_Access_TokenAsync());
}
//Console.Read();
//*/
//#endregion
////using HExpression;

////var ex = new HExpressionExtension();
////Console.WriteLine(ex.GetExpressionString()); 
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
//using ConsoleTestApp;

//ConfigurationBuilder configBuilder = new ConfigurationBuilder();
//configBuilder.AddJsonFile("app.json",optional:true,true);//optional-false:如果文件不存在则报错
//IConfigurationRoot configurationRoot = configBuilder.Build();
//////string name = configurationRoot["name"];
//////string studentName = configurationRoot.GetSection("Student:Name").Value;
////var student = configurationRoot.GetSection("Student").Get<Student>();
////Console.WriteLine(student.Name+","+student.Id);



//IServiceCollection services = new ServiceCollection();
//services.AddOptions().Configure<Student>(x => configurationRoot.GetSection("Student").Bind(x));
//services.AddScoped<ConfigService>();
//using (var sp=services.BuildServiceProvider())
//{
//    var c=sp.GetRequiredService<ConfigService>();
//    c.Test();
//}
Console.WriteLine("Hello");
//class Student
//{
//    public string Name { get; set; }
//    public int Id { get; set; }
//}
