# 表达式树Expression

#### 表达式树和委托在EF Core中的区别

Expression对象储存了运算逻辑,在运行时动态生成SQL语句,普通委托是查询全部数据后在内存执行

#### 查看Expression结构

1. 引入包`ExpressionTreeToString`
2. 使用扩展方法`ToString("Object notation","C#")`

#### 创建动态表达式树

1. 使用ToString("Factory methods","C#")类似工厂方法生成表达式树的代码

2. 适当调整生成的代码就可以简单创建表达式树

#### 附件

生成的代码为:

```
using static System.Linq.Expressions.Expression;
var x = Parameter(typeof(Book),"x");
Lambda(GreaterThan(MakeMemberAccess(x,typeof(Book).GetProperty("Price")),Constant(5)),x)
```

调整为:

```
var x = Parameter(typeof(Book), "x");var lambda=Lambda<Func<Book,bool>>(GreaterThan(MakeMemberAccess(x, typeof(Book).GetProperty("Price")), Constant(5.0)), x);
```

第三方库推荐:`System.Linq.Dynamic.Core`

可以使用字符串的语法来操作数据.例:

`var query=db.Customer.Where("Price==5 and Title=@0","测试").Select("new (ID,Name)")`
