namespace MiddleWare
{
    /// <summary>
    /// 自定义中间件类
    /// </summary>
    public class MiddleWare3
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 构造函数至少得RequestDelegate参数
        /// </summary>
        /// <param name="requestDelegate"></param>
        public MiddleWare3(RequestDelegate requestDelegate)
        {
            next = requestDelegate;
        }
        /// <summary>
        /// 定义InvokeAsync/Invoke方法  参数至少一个为HttpContext类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("MiddleWare3 Start<br />");
            await next.Invoke(context);
            await context.Response.WriteAsync("MiddleWare3 End<br />");
        }
    }
}
