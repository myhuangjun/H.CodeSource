# `MediatR`使用

#### 简介

*`MediatR`是.Net中开源的简单中介者模式实现,他通过一种进程内的消息传递机制,进行请求/响应,命令,查询,通知和事件的传递*

#### `Nuget`包

`MediatR.Extensions.Microsoft.DependencyInjection`

#### 简单使用步骤

1. 注册服务

   `services.AddMediatR(Assembly.GetExecutingAssembly());`

2. 推送消息

   ```C#
   private readonly IMediator mediator;
   public PushTest(IMediator mediator)
   {
   	this.mediator = mediator;
   }
   /// <summary>
   /// 推送消息
   /// </summary>
   /// <returns></returns>
   public string Push(string userName, string pwd)
   {
   	mediator.Publish(new UserInfo(userName,pwd);//Send用来发布一对一,Publish用来一对多
   	return $"ok:{userName},{pwd}";
   }
   ```

3. 创建传递参数`UserInfo`类,一般是`Record`类型,实现`INotification`接口

   ```C#
   internal record UserInfo(string UserName, string Password) : INotification;
   ```

4. 接收消息.实现`INotificationHandler<T>`接口的`Handle`方法

   ```C#
   public class EventMessage : INotificationHandler<UserInfo>
   {
   	Task INotificationHandler<UserInfo>.Handle(UserInfo notification, CancellationToken cancellationToken)
   	{
   		Console.WriteLine($"恭喜你,登录成功:{notification.UserName},{notification.Password}");
   		return Task.CompletedTask;
   	}
   }
   ```

#### 实现领域事件

1. 原理:借鉴`eShopOnContainers`项目的做法,把领域事件的发布放到上下文保存的时候发布,实体中注册要发布的领域事件

2. 定义`IDoMainEvent`接口,定义创建/获取/清空领域事件的方法

   ```C#
   internal interface IDoMainEvent
       {
           /// <summary>
           /// 获取所有注册的领域事件
           /// </summary>
           /// <returns></returns>
           List<INotification> GetAllEvents();
           /// <summary>
           /// 添加领域事件
           /// </summary>
           /// <param name="notification"></param>
           void AddEvent(INotification notification);
           /// <summary>
           /// 添加领域事件(避免重复添加)
           /// </summary>
           /// <param name="notification"></param>
           void AddNotExistEvent(INotification notification);
           /// <summary>
           /// 移除所有领域事件
           /// </summary>
           void RemoveEvent();
       }
   ```

3. 实体基类实现`IDoMainEvent`接口

   ```C#
   public class BaseEntity : IDoMainEvent
       {
           public List<INotification> Events=new List<INotification>();
           public void AddEvent(INotification notification)
           {
               Events.Add(notification);
           }
   
           public void AddNotExistEvent(INotification notification)
           {
               if(!Events.Contains(notification)) Events.Add(notification);
           }
   
           public List<INotification> GetAllEvents()
           {
               return Events;
           }
   
           public void RemoveEvent()
           {
               Events.Clear();
           }
   }
   ```

   

4. 重载`DbContext`中的`SaveChangesAsync/SaveChanges`方法

   ```C#
   internal class MyDbContext : DbContext
   {
   	private readonly IMediator mediator;
   	public MyDbContext(IMediator mediator)
   	{
           this.mediator = mediator;
   	}
   	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
   	{
       //找出所有继承自BaseEntity 并且包含未发布事件的对象
   	var domainEnties = this.ChangeTracker.Entries<BaseEntity>().Where(x => x.Entity.GetAllEvents().Any());
       //查找所有注册的事件
   	var domainEvents = domainEnties.SelectMany(x => x.Entity.GetAllEvents()).ToList();
   	//保存数据
       var result= base.SaveChangesAsync(cancellationToken);
       //发送消息
   	domainEvents.ForEach(async x => await mediator.Publish(x, cancellationToken));
   	//清空消息
       domainEnties.ToList().ForEach(x => x.Entity.RemoveEvent());
   	return await result;
   }
   ```

   

5. 创建领域事件类`UserInfo`,继承`BaseEntity`,在新增用户时注册事件

   ```C#
   internal class User : BaseEntity
   {
   	public int Id { get; set; }
   	public string Name { get; set; }
   	public string Email { get; set; }
   	public void AddUser(string name, string email)
   	{
   		this.Name = name;
   		this.Email = email;
   		AddEvent(new AddUserInfo(name, email));
   	}
   }
   ```

6. 创建领域事件传递类`AddUserInfo`

   `public record AddUserInfo(string name, string email) : INotification;`

7. 创建事件处理类`AddUserHandler`

   ```C#
   public class AddUserHandler : INotificationHandler<AddUserInfo>
   {
   	public Task Handle(AddUserInfo notification, CancellationToken cancellationToken)
   	{
           //dosomething.....
   		Console.WriteLine($"我已经收到消息了,你注册成功了{notification.name}");
   		return Task.CompletedTask;
   	}
   }
   ```

#### 附录

- 在`.Net EF Core`中通过`MediatR`实现领域事件已封装成包,需要的小伙伴可以引入包`HMediatR `
- 相关文档可在[HMediatR · Huang/NetCorePackages - 码云 - 开源中国 (gitee.com)](https://gitee.com/Huang_Jun/net-core-packages/tree/master/HMediatR)查看



