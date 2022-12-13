using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    /// <summary>
    /// 演示一对多 单向导航
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
    }

    public class Leave
    {
        public int Id { get; set; }
        public User AuditUser { get; set; }

        public int AuditUserId { get; set; }
        public User OperUser { get; set; }

        public int OperUserId { get; set; }
    }
}
