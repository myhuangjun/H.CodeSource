//// See https://aka.ms/new-console-template for more information

//using H.EnterpriseWechatInterface;
//using H.Logger;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//Console.WriteLine("Hello, World!");
//IServiceCollection services = new ServiceCollection();
//services.AddScoped<Logger>();
//services.AddEnterpriseWechat();
//using (var sp=services.BuildServiceProvider())
//{
//    var service= sp.GetService<IEnterpriseWechatInterface>();
//    Console.WriteLine(service.Get_Access_TokenAsync());
//}
//Console.Read();
using HConfiguration;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var configurationBuilder = new ConfigurationBuilder();
//configurationBuilder.AddJsonFile("app.json",false,true);
//configurationBuilder.Add(new HConfigurationSource() { Path = "app.json",ReloadOnChange=true });
IConfigurationRoot root = configurationBuilder.Build();
Console.WriteLine(root["name"] + "::"+root["age"]); 