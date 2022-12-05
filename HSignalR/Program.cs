using HSignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
//builder.Services.AddSignalR().AddStackExchangeRedis("RedisµØÖ·", opt =>
//{
//    opt.Configuration.ChannelPrefix = "Ç°×º"; 
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHub<HChatRoomHub>("/HHub");

app.UseStaticFiles();

app.MapControllers();

app.Run();
