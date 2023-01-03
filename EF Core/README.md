#### 一.迁移数据库

1.引入包`Microsoft.EntityFrameworkCore.SqlServer`
2.(可选)建配置类,实现`IEntityTypeConfiguration<T>`[`Fluent Api`]

```C#
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.ToTable("Books");
	}
}
```

3.创建上下文,继承自`DbContext`类

```C#
public class MyDbContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        /// <summary>
        /// 配置链接字符串
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "";
            optionsBuilder.UseSqlServer(conn);
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 配置 实体配置类的位置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
```

4. 迁移数据库,安装包`Microsoft.EntityFrameworkCore.Tools`

   1. Add-Migration xxxx
   2. Update-database

#### 二.增删改查

```C#
using var context = new MyDbContext();
//数据新增
var book = new Book() { };
context.Books.Add(book);
var result = await context.SaveChangesAsync();//保存
Console.WriteLine("成功执行条数:" + result);
//查询数据
var list = context.Books.Where(x => x.Id == 1);
//修改数据
var b = context.Books.Single(x => x.Id == 2);
b.Price = xxxx;
await context.SaveChangesAsync();
//删除数据
var c = context.Books.Single(x => x.Id == 2);
context.Books.Remove(c);
await context.SaveChangesAsync();
```

#### 三.配置方式

1. 特性配置
2. `Fluent Api`

#### 四.深入Migration

1. `Update-Database migrationName` 回滚到某个migration的状态
2. `Remove-migration`  删除最后一次迁移
3. `Script-Migration D F`  生成D到F版本的Sql语句,但不执行

#### 五.查看Sql语句

1. 标准日志

   ```C#
   private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(c => c.AddConsole());
   
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
   	string conn = "******";
   	optionsBuilder.UseSqlServer(conn);
   	optionsBuilder.UseLoggerFactory(loggerFactory);  //配置日志系统
   	base.OnConfiguring(optionsBuilder);
   }
   ```

2. 简单日志

   ```c#
   optionsBuilder.LogTo(Console.WriteLine);//简单日志
   ```

3. `IQueryable`的`ToQueryString`方法

   ```C#
   var list = context.Books.Where(x => x.Id == 1);
   Console.WriteLine(list.ToQueryString()); 
   ```

#### 六.使用Mysql数据库

1. `EF Provider`选择:`MySQL.EntityFrameworkCore --MySql官方`,`Pomelo.EntityFrameworkCore.MySql   --开源开发者`

2. EF原理:`EF Core`->数据库Provider->`Ado.Net Core`->数据库

#### 七.导航属性

1. 一对多关系设置
    `评论端设置:builder.HasOne<Article>(x => x.Article).WithMany(a=>a.Comments).IsRequired();`

2. 获取一对多的关系:**Include**
    `var a1 = ctx.Articles.Include(x => x.Comments).Single(x => x.Id == 1);`

3. 一对多的关系:`WithMany`不带参数即可

  ```C#
   //单向导航  显示指定外键并加上.OnDelete(DeleteBehavior.Restrict)
  builder.HasOne<User>(x =>x.AuditUser).WithMany().
  HasForeignKey(x=>x.AuditUserId).OnDelete(DeleteBehavior.Restrict);
  ```

#### 八.`IQueryable`的延迟查询

1. `IQueryable`终结方法才会查出数据:终结方法:`遍历,ToArray().ToList(),Min().Max(),Count()等`;非终结方法:`GroupBy(),OrderBy(),Include(),Skip(),Take()等(一般返回IQueryable)`

2. 可以利用延迟查询实现动态查询

3. 默认使用的是`DataReader`方式,一直连接数据库读取;可以使用`ToList()等`方法把全部数据读取到内存

4. 异步方法.大部分是定义在`Microsoft.EntityFrameworkCore`命名空间下,终端方法才有异步方法

5. 异步遍历`IQueryable`

   ```C#
   await foreach(var item in ctx.Article.AsAsyncEnumable())
   {
       Console.WriteLine(item.Title);
   }
   ```

#### 九.执行原生SQL语句

1. 执行查询方法:`ctx.Database.ExcuteSqkInterpolatedAsync()`,参数用`FormattableString`可以避免SQL注入

2. 实体对应sql查询:`IQueryable<Book> books=ctx.Book.FromSqlInterpolated()`

3. 你用ADO.NET执行复杂SQL

   ```C#
   var conn = ctx.Database.GetDbConnection();
   if (conn.State != System.Data.ConnectionState.Open) conn.Open();
   using var cmd = conn.CreateCommand();
   cmd.CommandText = "select * from  Articles";
   cmd.CommandType = System.Data.CommandType.Text;
   using var reader = await cmd.ExecuteReaderAsync();
   while (await reader.ReadAsync())
   {
       Console.WriteLine($"{reader[0]}--{reader[1]}--{reader[2]}");
   }
   ```
4. 推荐使用Dapper等框架执行原生复杂查询SQL
#### 十.实体跟踪
1. 调用`ctx.Entry(a1);`查看实体在EF的跟踪信息,`DebugView.LongView`可以查看快照信息
1. 如果查询的数据不做修改,可以使用`AsNoTracking()`降低内存占用
1. 





#### 附录:

1. 微软官方文档:https://learn.microsoft.com/zh-cn/aspnet/core/data/ef-mvc/intro?source=recommendations&view=aspnetcore-6.0

2. `Guid`设置主键时不能用聚集索引,在`MySQL`中,插入频繁的表不要用`Guid`做主键

