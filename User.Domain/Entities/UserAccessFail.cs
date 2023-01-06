using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public record UserAccessFail
    {
        public Guid Id { get; init; }
        public User User { get; init; }
        public Guid UserId { get; init; }
        private bool isLockOut;
        public DateTime? LockEnd { get; private set; }
        public int AccessFailCount { get; private set; }
        private UserAccessFail() { }
        public UserAccessFail(User user)
        {
            User = user;
            this.Id = Guid.NewGuid();
        }
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            this.AccessFailCount = 0;
            this.isLockOut = false;
            this.LockEnd = null;
        }
        /// <summary>
        /// 失败
        /// </summary>
        public void Fail()
        {
            this.AccessFailCount++;
            if (this.AccessFailCount < 3)
            {
                this.isLockOut = true;
                this.LockEnd = DateTime.Now.AddMinutes(5);
            }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        /// <returns></returns>
        public bool IsLockOut()
        {
            if (this.isLockOut)
            {
                if (DateTime.Now > this.LockEnd) { Reset(); return false; }
                else return true;
            }
            return true;
        }
    }
}
