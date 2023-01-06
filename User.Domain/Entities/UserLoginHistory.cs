﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public class UserLoginHistory:IAggregateRoot
    {
        public long Id { get; init; }
        public Guid? UserId { get; init; }
        public PhoneNumber PhoneNumber { get;init; }
        public DateTime CreatedTime { get; init; }
        public string Message { get; init; }
        private UserLoginHistory() { }
        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber,  string message)
        { 
            UserId = userId;
            PhoneNumber = phoneNumber;
            CreatedTime = DateTime.Now; ;
            Message = message;
        }
    }
}
