# HRedis

#### 用途

-  可方便快捷的操作Redis,现支持string,Hash


#### 使用步骤

​	1.注册服务

```C#
services.AddHRedis(opt =>
{
    opt.ConnectionString = "************";
    opt.DefaultKey = "Test";
    opt.Db = 10;//默认-1
});
```

​	2.使用方法

```C#
//设置string
var f = redis.StringSet("01", "测试01");
Console.WriteLine(f?"设置成功":"设置失败");
//读取string,不存在则创建
var value = redis.StringGet("02", null, () =>
{
    return new {Name="测试02"};
});
Console.WriteLine(value);
//设置Hash
var dic = new Dictionary<string, string>
{
    { "01", "{\"Name\":\"张三\"}" },
    { "02", "{\"Name\":\"李四\"}" }
};
var f= redis.SetHash("Student", dic);
Console.WriteLine(f?"设置成功":"设置失败");
//读取Hash,不存在则创建
var student = redis.GetHash("Student", "03", () =>
{
    var value = "{\"Name\":\"王五\"}";
    return value;
});
```

