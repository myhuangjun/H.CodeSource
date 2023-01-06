using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Domain;
using User.Infrastracture;
using User.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<UserDbContext>(o =>
{
    o.UseSqlServer("Data Source=192.168.1.20;Database=ManagerSystem2;UID=sa;PWD=mrj_5678987;MultipleActiveResultSets=true");
});
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<IUserRepository, UserResposity>();
builder.Services.AddScoped<ISmsCodeSender, SmsCodeSender>();
builder.Services.Configure<MvcOptions>(o =>
{
    o.Filters.Add<UnitOfWorkFilter>();
}); //×¢²áÈ«¾ÖFilter

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
