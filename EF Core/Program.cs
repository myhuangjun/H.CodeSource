// See https://aka.ms/new-console-template for more information
using EF_Core;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

#region 数据的增删改查
//using var context = new MyDbContext();
using var ctx = new MyDbContext();
////数据新增
//var book = new Book() { Title = ".Net入门到入狱", Price = 120.0, Content = "点击详情查看更多", PublicTime = DateTime.Now };
//context.Books.Add(book);
//book = new Book() { Title = ".Net从精通到陌生", Price = 13.0, Content = "点击详情查看更多", PublicTime = DateTime.Now };
//context.Books.Add(book);
//book = new Book() { Title = ".Net从入门到放弃", Price = 20.0, Content = "点击详情查看更多", PublicTime = DateTime.Now };
//context.Books.Add(book);
//var result = await context.SaveChangesAsync();//保存
//Console.WriteLine("成功执行条数:" + result);
//查询数据
//var list = context.Books.Where(x => x.Id == 1);
//Console.WriteLine(list.ToQueryString()); 
//foreach (var item in list)
//{
//    Console.WriteLine(item.Price);
//}
////修改数据
//var b = context.Books.Single(x => x.Id == 2);
//b.Price = 999;
//await context.SaveChangesAsync();

////删除数据 
//var c = context.Books.Single(x => x.Id == 2);
//context.Books.Remove(c);
//await context.SaveChangesAsync();
#endregion

#region 多对多的新增

//var article = new Article() { Content = "今天疫情放开了", Title = "2022-12-9" };
//var c1 = new Comment() { Message = "没想到是这种方式结束" };
//var c2 = new Comment() { Message = "我们是不是更危险了" };
//article.Comments.Add(c1);
//article.Comments.Add(c2);
//ctx.Articles.Add(article);
//await ctx.SaveChangesAsync();
//var a1 = ctx.Articles.Include(x => x.Comments).Single(x => x.Id == 1);
//Console.WriteLine(a1.Title);
//a1.Comments.ForEach(x => Console.WriteLine(x.Message));
#endregion
#region 执行原生SQL
//var conn = ctx.Database.GetDbConnection();
//if (conn.State != System.Data.ConnectionState.Open) conn.Open();
//using var cmd = conn.CreateCommand();
//cmd.CommandText = "select * from  Articles";
//cmd.CommandType = System.Data.CommandType.Text;
//using var reader = await cmd.ExecuteReaderAsync();
//while (await reader.ReadAsync())
//{
//    Console.WriteLine($"{reader[0]}--{reader[1]}--{reader[2]}");
//}
//var list=ctx.Articles.Where(x=>x.Id>0).ToList();
//list.ForEach(x => Console.WriteLine($"{x.Id}--{x.Content}"));
//Console.WriteLine("**************");
////忽略筛选器
//var list2 = ctx.Articles.IgnoreQueryFilters().Where(x => x.IsDelete).ToList();
//list2.ForEach(x => Console.WriteLine($"{x.Id}--{x.Content}"));
#endregion

#region 乐观锁
Console.ReadLine();
Console.WriteLine("开始执行");
var article=ctx.Articles.First();
article.IsDelete = !article.IsDelete;//IsDelete取反
Thread.Sleep(5000);
try
{
	ctx.SaveChanges();
}
catch (DbUpdateConcurrencyException e)
{
	var entry = e.Entries.First();

	Console.WriteLine("并发访问冲突,新值为:" + entry.GetDatabaseValues().GetValue<bool>("IsDelete"));
	Console.ReadLine();
	return;
}
Console.WriteLine("设置成功");
Console.ReadLine();
#endregion