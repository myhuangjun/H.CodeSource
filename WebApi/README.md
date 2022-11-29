# Filter

#### ASP.NET Core中的Filter的五种类型

##### Authorization filter,Resource filter,<u>Action filter,Exception filter</u>,Result filter

#### Exception filter

1. 继承`IAsyncExceptionFilter`

   ```
   public class HExceptionFilter : IAsyncExceptionFilter
   {
   	/// <summary>
   	/// 异常处理
   	/// </summary>
   	/// <param name="context"></param>
   	/// <returns></returns>
   	public Task OnExceptionAsync(ExceptionContext context)
   	{
   		//context.Exception  异常信息对象
   		context.ExceptionHandled = true;  //异常信息是否被处理
   		//context.Result = "";返回给浏览器信息
   		ObjectResult obj = new ObjectResult(new { code = 500, message = "服务端发生错误" });
   		context.Result = obj;
   		return Task.CompletedTask;
   	}
   }
   ```

2. DI注册

      ```
      builder.Services.Configure<MvcOptions>(opt =>
      {
          opt.Filters.Add<HExceptionFilter>();
      });
      ```

#### Action Filter

1.继承`IAsyncActionFilter`

```c#
public class HActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("Filter执行之前");
            ActionExecutedContext result = await next();
            if(result.Exception!=null)
            {
                Console.WriteLine("发生异常了");
            }
            else
            {
                Console.WriteLine("Filter执行之后");
            }
        }
    }
```

2. DI注册服务

   ```C#
   builder.services.Configure<MvcOptions>(opt=>{
   	opt.Filter.Add<HActionFilter>();
   })
   ```

#### 实例:自动启用事务

1.利用`TransactionScope`创建对象

```C#
public class HTransationScopeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool hasNotTansation = false;
            if(context.ActionDescriptor is ControllerActionDescriptor)
            {
                var actionDesc = (ControllerActionDescriptor)context.ActionDescriptor;
                hasNotTansation = actionDesc.MethodInfo.IsDefined(typeof(NotTransactionAttribute),false);
            }
            if (hasNotTansation)
            {
                await next.Invoke();
                return; 
            }
            using var score = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = await next.Invoke();
            if (result.Exception != null) score.Complete();
        }
    }
	///不需要被事务的Action
    [AttributeUsage(AttributeTargets.Method)]
    public class NotTransactionAttribute : Attribute
    {

    }
```

2. DI注册服务

   ```C#
   builder.services.Confige<MvcOptions>(opt=>{
       opt.Filter.Add<HTransationScopeFilter>();
   })
   ```

   
