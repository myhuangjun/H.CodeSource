//using HCommon;

namespace User.Domain.Entities
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public PhoneNumber PhoneNumber { get;private set; }
        private string? PasswordHash { get; set; }
        public UserAccessFail UserAccessFail { get;private set; }

        public User() { }
        public User(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
            Name = phoneNumber.Number;
            //PasswordHash = EncryHelper.Md5("123456");
            this.Id=Guid.NewGuid();
            this.UserAccessFail=new UserAccessFail(this);
        }
        /// <summary>
        /// 判断是否设置密码
        /// </summary>
        /// <returns></returns>
        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(PasswordHash);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ChangePassword(string password)
        {
            if (password.Length < 3) throw new ArgumentOutOfRangeException("密码长度必须大于3");
            //this.PasswordHash = EncryHelper.Md5(password);
        }
        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        public  void ChangePhone(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
    }
}