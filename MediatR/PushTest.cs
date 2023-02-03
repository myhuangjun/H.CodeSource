using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMediatR
{
    internal class PushTest
    {
        private readonly IMediator mediator;

        public PushTest(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// 推送消息
        /// </summary>
        /// <returns></returns>
        public string Push(string userName, string pwd)
        {
            mediator.Publish(new UserInfo(userName,pwd ));
            return $"ok:{userName},{pwd}";
        }
    }

    public class EventMessage : INotificationHandler<UserInfo>
    {
        Task INotificationHandler<UserInfo>.Handle(UserInfo notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"恭喜你,登录成功:{notification.UserName},{notification.Password}");
            return Task.CompletedTask;
        }
    }

    public class EventInfo : INotificationHandler<UserInfo>
    {
        Task INotificationHandler<UserInfo>.Handle(UserInfo notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{notification.UserName}你有新的消息");
            return Task.CompletedTask;
        }
    }
}
