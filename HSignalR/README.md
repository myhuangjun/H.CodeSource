# SignalR

#### 基本SignalR项目

1. 继承自Hub

```C#
public class HChatRoomHub:Hub
    {
        public Task PublicSendMessage(string message)
        {
            string connId = this.Context.ConnectionId;
            var msg = $"{connId}:{DateTime.Now}:{message}";
            return Clients.All.SendAsync("PublicMsgReceived",msg);
        }
    }
```

2. 注册服务

```C#
builder.Services.AddSignalR();
app.MapHub<HChatRoomHub>("/HHub");
```

#### SignalR分布式

1. 多个服务端之间消息互通

2. 官方方案:`Redis backplane`

3. 引入包`Microsoft.AspNetCore.SignalR.StackExchangeRedis`

4. 添加引用

   ```C#
   builder.Services.AddSignalR().AddStackExchangeRedis("Redis地址", opt =>
   {
       opt.Configuration.ChannelPrefix = "前缀";
   });
   ```



