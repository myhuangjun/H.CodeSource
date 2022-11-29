namespace MiddleWare
{
    public class NotFoundMiddleWare
    {
        private readonly RequestDelegate next;
        public NotFoundMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync("没有找到页面");

        }
    }
}
