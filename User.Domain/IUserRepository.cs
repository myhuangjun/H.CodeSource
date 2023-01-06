using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain
{
    public interface IUserRepository
    {
        public Task<Entities.User?> FindOneAsync(PhoneNumber phoneNumber);
        public Task<Entities.User?> FindOneAsync(Guid userId);
        public Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message);

        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code);
        public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber);

        public Task PublishEventAsync(UserAccessResultEvent userAccessResultEvent);
        public Task AddUser(PhoneNumber phoneNumber);
    }
}
