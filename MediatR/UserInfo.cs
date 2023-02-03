using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMediatR
{
    //internal record UserInfo(string UserName, string Password) : INotification;
    public class UserInfo: INotification
    {
        public UserInfo(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
