using MiddleWare;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

#region �򵥵��м��
app.Map("/Test", async appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("1 start<br />");
        await next();
        await context.Response.WriteAsync("1 end<br />");
    });

    appBuilder.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("2 start<br />");
        await next();
        await context.Response.WriteAsync("2 end<br />");
    });
    appBuilder.UseMiddleware<MiddleWare3>();

    //���һ��ִ�е��м��
    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsync("Run<br />");
    }); 
});
#endregion

app.UseMiddleware<MarkdownMiddleWare>();

app.UseMiddleware<NotFoundMiddleWare>();



app.Run();
