using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core
{
    internal class MyDbContext:DbContext
    {
        public DbSet<Book> Books { get; set; }

        //private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(c => c.AddConsole());

        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Leave> Leaves { get; set; }
        
        /// <summary>
        /// 配置链接字符串
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "*********************************************";
            optionsBuilder.UseSqlServer(conn);
            //optionsBuilder.UseLoggerFactory(loggerFactory);  //配置日志系统
            //optionsBuilder.LogTo(Console.WriteLine);//简单日志
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
}
