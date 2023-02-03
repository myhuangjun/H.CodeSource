using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMediatR
{
    internal class MyDbContext : DbContext
    {
        private readonly IMediator mediator;

        public MyDbContext(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "Data Source=192.168.1.20;Database=ManagerSystem2;UID=sa;PWD=mrj_5678987;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(conn);
            optionsBuilder.LogTo(Console.WriteLine, minimumLevel: LogLevel.Information);//简单日志
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEnties = this.ChangeTracker.Entries<BaseEntity>().Where(x => x.Entity.GetAllEvents().Any());//找出所有继承自BaseEntity 并且包含未发布事件的对象
            var domainEvents = domainEnties.SelectMany(x => x.Entity.GetAllEvents()).ToList();//查找所有注册的事件
            var result= base.SaveChangesAsync(cancellationToken); //保存数据库
            domainEvents.ForEach(async x => await mediator.Publish(x, cancellationToken));//发送消息
            domainEnties.ToList().ForEach(x => x.Entity.RemoveEvent());//清空消息
            return await result;
        }
    }
}
