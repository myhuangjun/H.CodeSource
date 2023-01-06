using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Domain
{
    public class UserDomainService
    {
        private IUserRepository userRepository;
        private ISmsCodeSender smsCodeSender;
        public UserDomainService(IUserRepository userRepository, ISmsCodeSender smsCodeSender)
        {
            this.userRepository = userRepository;
            this.smsCodeSender = smsCodeSender;
        }

        public void ResetAccessFail(Entities.User user)
        {
            user.UserAccessFail.Reset();
        }

        public bool IsLockOut(Entities.User user)
        {
            return user.UserAccessFail.IsLockOut();
        }

        public void AccessFail(Entities.User user)
        {
            if (user == null) return;
            user.UserAccessFail.Fail();
        }

        public async Task<UserAccessResult> CheckPassword(PhoneNumber phoneNumber, string password)
        {
            var result = new UserAccessResult();
            var user = await userRepository.FindOneAsync(phoneNumber);
            if (user == null) result = UserAccessResult.PhoneNumberNotFound;
            else if (IsLockOut(user)) result = UserAccessResult.LockOut;
            else if (!user.HasPassword()) result = UserAccessResult.NotPassword;
            else result = UserAccessResult.OK;
            if (result == UserAccessResult.OK)
            {
                ResetAccessFail(user);
            }
            else
            {
                AccessFail(user);
            }
            await userRepository.PublishEventAsync(new UserAccessResultEvent(phoneNumber, result));
            return result;
        }
    }
}
