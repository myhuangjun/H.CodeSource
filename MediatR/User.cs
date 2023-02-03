using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMediatR
{
    internal class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public void AddUser(string name, string email)
        {
            this.Name = name;
            this.Email = email;
            AddEvent(new AddUserInfo(name, email));
        }

    }
    public record AddUserInfo(string name, string email) : INotification;


    public class AddUserHandler : INotificationHandler<AddUserInfo>
    {
        public Task Handle(AddUserInfo notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"我已经收到消息了,你注册成功了{notification.name}");
            return Task.CompletedTask;
        }
    }
}
