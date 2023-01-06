using MediatR;
using User.Domain;
using User.Infrastracture;

namespace User.WebApi
{
    public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserRepository userRepository;
        private readonly UserDbContext dbContext;
        public UserAccessResultEventHandler(IUserRepository userRepository, UserDbContext dbContext)
        {
            this.userRepository = userRepository;
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            await userRepository.AddNewLoginHistoryAsync(notification.PhoneNumber, $"登录结果{notification.Result.ToString()}");
            await dbContext.SaveChangesAsync();
        }

    }
}
