# 企业微信接口

#### 已支持接口

1. 获取Token:`Get_Access_TokenAsync`

2. 推送消息给某人:`SendMessageAsync`

3. 通过机器人推送消息到群聊:`SendRobotMessageAsync`

#### 使用方式

 1. 注册服务

    ```C#
    services.AddEnterpriseWechat(set => {
        set.Wechat_Secret = "****Secret";
        set.Wechat_Corp_Id = "*****Corp_Id*******";
        set.Wechat_AgentId = "********AgentId********";
    });
    ```

 2. 使用

```C#
using (var sp = services.BuildServiceProvider())
{
    var service = sp.GetRequiredService<IEnterpriseWechatInterface>();
    Console.WriteLine(service.Get_Access_TokenAsync());
}
```

