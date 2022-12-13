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

4. 

5. 



   





#### 附录:

1. 微软官方文档:https://learn.microsoft.com/zh-cn/aspnet/core/data/ef-mvc/intro?source=recommendations&view=aspnetcore-6.0

2. `Guid`设置主键时不能用聚集索引,在`MySQL`中,插入频繁的表不要用`Guid`做主键

