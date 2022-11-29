using MarkdownSharp;
using System.Text;

namespace MiddleWare
{
    public class MarkdownMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment hostEnvironment;

        public MarkdownMiddleWare(RequestDelegate next, IWebHostEnvironment hostEnvironment)
        {
            this.next = next;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            var file = hostEnvironment.WebRootFileProvider.GetFileInfo(path);
            if (!file.Exists)
            {
                await next(context);
                return;
            }
            using var stream = file.CreateReadStream();
            //查询文件编码
            Ude.CharsetDetector detector = new Ude.CharsetDetector();
            detector.Feed(stream);
            detector.DataEnd();
            string charset = detector.Charset ?? "UTF-8";
            stream.Position = 0;
            using StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
            var text = await reader.ReadToEndAsync();
            //md文件转为html
            Markdown md = new Markdown();
            var html = md.Transform(text);
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(html);
        }
    }
}
