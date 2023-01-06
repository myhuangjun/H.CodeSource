using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain;
using User.Domain.Entities;

namespace User.Infrastracture
{
    public class SmsCodeSender : ISmsCodeSender
    {
        public Task SendCodeAsync(PhoneNumber phoneNumber, string code)
        {
            Console.WriteLine($"发送短信{phoneNumber.Number},验证码为:{code}");
            return Task.CompletedTask;
        }
    }
}
