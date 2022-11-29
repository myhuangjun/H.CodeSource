# 配置系统

#### 原始方法

 1. NugGet安装`Microsoft.Extensions.Configuration`和`Microsoft.Extensions.Configuration.Json` 

 2. 设置配置文件属性为:始终复制

 3. 读取配置原始方法

    ```
    ConfigurationBuilder configBuilder = new ConfigurationBuilder();
    configBuilder.AddJsonFile("app.json",optional:true,true);//optional-false:如果文件不存在则报错
    IConfigurationRoot configurationRoot = configBuilder.Build();
    string name = configurationRoot["name"];
    string studentName = configurationRoot.GetSection("Student:Name").Value;
    ```

4. 绑定读取配置

    1. NuGet安装`Microsoft.Extensions.Configuration.Binder`
    2. `var student = configurationRoot.GetSection("Student").Get<Student>();`


#### 选项方式读取配置(可以通过DI注入)

1. NuGet额外安装`Microsoft.Extensions.Options`

2. DI注册服务

   ```
   IServiceCollection services = new ServiceCollection();
   services.AddOptions().Configure<Student>(x => configurationRoot.GetSection("Student").Bind(x));
   ```

3. 使用

   ```
   /// <summary>
   /// IOptionsSnapshot--在同一范围类保持一致   IOtions<T>  不会读取新的值
   /// </summary>
   private readonly IOptionsSnapshot<Student> optionsSnapshot;
   public ConfigService(IOptionsSnapshot<Student> optionsSnapshot)
   {
        this.optionsSnapshot = optionsSnapshot;
   }
   public void Test()
   {
       Console.WriteLine($"{optionsSnapshot.Value.Name}********{optionsSnapshot.Value.Id}");
   }

#### 其他配置提供者

##### 命令行配置

1. NuGet安装包`Microsoft.Extensions.Configuration.CommandLine`
1. `ConfigurationBuilder`添加方法

```
ConfigurationBuilder configBuilder = new ConfigurationBuilder();
configBuilder.AddCommandLine(args)
```

3. 扁平化配置 `Student:name="张三",Student:age=18`

##### 环境变量
