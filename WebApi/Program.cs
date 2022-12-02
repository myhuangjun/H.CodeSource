using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//注册托管服务
builder.Services.AddHostedService<HHostedService>();
builder.Services.AddScoped<TestService>();

builder.Services.AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());
});

builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<HExceptionFilter>();
    opt.Filters.Add<HActionFilter>();
    opt.Filters.Add<HTransationScopeFilter>();
});
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.InstanceName = "前缀";
    opt.Configuration = "配置";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
