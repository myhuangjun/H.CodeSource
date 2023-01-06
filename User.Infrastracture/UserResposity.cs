using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Domain;
using User.Domain.Entities;

namespace User.Infrastracture
{
    public class UserResposity : IUserRepository
    {
        public readonly UserDbContext dbContext;
        public readonly IMediator mediator;

        public UserResposity(UserDbContext dbContext, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
        }

        public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string message)
        {
            Domain.Entities.User? user = await FindOneAsync(phoneNumber);
            await dbContext.LoginHistories.AddAsync(new UserLoginHistory(user?.Id, phoneNumber, message));
        }

        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            var user=dbContext.Users.Include(x=>x.UserAccessFail).SingleOrDefault(x=>x.PhoneNumber.RegionCode==phoneNumber.RegionCode&&x.PhoneNumber.Number==phoneNumber.Number);
            return Task.FromResult(user);
        }

        public async Task<Domain.Entities.User?> FindOneAsync(Guid userId)
        {
            var user =await dbContext.Users.Include(x => x.UserAccessFail).SingleOrDefaultAsync(x => x.Id==userId);
            return user;
        }

        public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task PublishEventAsync(UserAccessResultEvent userAccessResultEvent)
        {
            return mediator.Publish(userAccessResultEvent);
        }

        public async Task AddUser(PhoneNumber phoneNumber)
        {
            await dbContext.Users.AddAsync(new Domain.Entities.User(phoneNumber));
        }
    }
}