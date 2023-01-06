using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain
{
    public interface ISmsCodeSender
    {
        public Task SendCodeAsync(PhoneNumber phoneNumber, string code);
    }
}
