﻿// See https://aka.ms/new-console-template for more information
using EF_Core;

Console.WriteLine("Hello, World!");

#region 数据的增删改查
//using var context = new MyDbContext();
////数据新增
//var book = new Book() { Title = ".Net入门到入狱", Price = 120.0, Content = "点击详情查看更多", PublicTime = DateTime.Now };
//context.Books.Add(book);
//book = new Book() { Title = ".Net从精通到陌生", Price = 13.0, Content = "点击详情查看更多", PublicTime = DateTime.Now };
//context.Books.Add(book);
//book = new Book() { Title = ".Net从入门到放弃", Price = 20.0, Content = "点击详情查看更多", PublicTime = DateTime.Now };
//context.Books.Add(book);
//var result = await context.SaveChangesAsync();//保存
//Console.WriteLine("成功执行条数:" + result);
////查询数据
//var list = context.Books.Where(x => x.Id == 1);
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
