// See https://aka.ms/new-console-template for more information
using HMediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

Console.WriteLine("Hello, World!");
IServiceCollection services = new ServiceCollection();
services.AddMediatR(Assembly.GetExecutingAssembly());
services.AddScoped<MyDbContext>();
//services.AddScoped<PushTest>();
//IServiceProvider sp = services.BuildServiceProvider();
//var test = sp.GetService<PushTest>();
//test.Push("张三", "132456");
//Console.ReadLine();
IServiceProvider sp = services.BuildServiceProvider();
using var ctx = sp.GetService<MyDbContext>();
var user = new User();
user.AddUser("李四","123456@qq.com");
ctx.Users.Add(user);
await ctx.SaveChangesAsync();