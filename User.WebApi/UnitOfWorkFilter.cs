using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace User.WebApi
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception != null)//只有执行成功才调用SaveChanges
                return;
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDesc == null) return;
            var attr = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            if (attr == null) return;
            foreach (var ctxType in attr.DbContext)
            {
                var dbCtx = context.HttpContext.RequestServices.GetService(ctxType) as DbContext;
                if (dbCtx == null) continue;
                await dbCtx.SaveChangesAsync();
            }
        }
    }
}
