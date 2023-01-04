using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    /// <summary>
    /// 演示一对多
    /// </summary>
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsDelete { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }


    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Article Article { get; set; }
    }
}
