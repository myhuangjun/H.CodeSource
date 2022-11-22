using ExpressionTreeToString;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace HExpression
{
    /// <summary>
    /// 表达式扩展
    /// </summary>
    public class HExpressionExtension
    {
        public string GetExpressionString()
        {
            //Expression<Func<Book, bool>> expression = x => x.Title == "高级";
            //return expression.ToString("Factory methods", "C#");

            Expression<Func<Book, bool>> expression2 = x => x.Price == 5;
            return expression2.ToString("Factory methods", "C#");//"Object notation", "C#"
        }
        /// <summary>
        /// 动态获取满足条件的数据(等于的情况)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList<T>(string propertyName, object value)
        {
            var valueType = typeof(T).GetProperty(propertyName).PropertyType;
            Expression<Func<T, bool>> express;
            var pare = Parameter(typeof(T), "x");
            var left = MakeMemberAccess(pare, typeof(T).GetProperty("Price"));
            var right = Constant(System.Convert.ChangeType(value, valueType));
            if (valueType.IsGenericType)
            {
                express = Lambda<Func<T, bool>>(Equal(left, right), pare);
            }
            else
            {
                express = Lambda<Func<T, bool>>(MakeBinary(ExpressionType.Equal, left, right, false, typeof(string).GetMethod("op_Equality")), pare);
            }
            //todo 等学习了EF Core后继续操作
            return null;
        }

        /// <summary>
        /// 自定义构建列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public IEnumerable<object> QueryProp<T>(params string[] propertyNames) where T:class
        {
            var p = Parameter(typeof(T));
            List<Expression> propExpList = new List<Expression>();
            foreach (string propertyName in propertyNames)
            {
                Expression exp = Convert(MakeMemberAccess(p, typeof(T).GetProperty(propertyName)), typeof(object));
                propExpList.Add(exp);
            }
            var newArrayExpr=NewArrayInit(typeof(object),propExpList.ToArray());
            return null;
            //var selectExp = Lambda<Func<T, object[]>>(newArrayExpr, p);
            //using (var db=new DbContext())
            //{
            //    return db.Set<T>.Select(selectExp).ToArray();
            //}
        }
    }


    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }
}