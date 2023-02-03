using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMediatR
{
    internal interface IDoMainEvent
    {
        /// <summary>
        /// 获取所有注册的领域事件
        /// </summary>
        /// <returns></returns>
        List<INotification> GetAllEvents();
        /// <summary>
        /// 添加领域事件
        /// </summary>
        /// <param name="notification"></param>

        void AddEvent(INotification notification);
        /// <summary>
        /// 添加领域事件(避免重复添加)
        /// </summary>
        /// <param name="notification"></param>
        void AddNotExistEvent(INotification notification);
        /// <summary>
        /// 移除所有领域事件
        /// </summary>
        void RemoveEvent();
    }


    public class BaseEntity : IDoMainEvent
    {
        public List<INotification> Events=new List<INotification>();
        public void AddEvent(INotification notification)
        {
            Events.Add(notification);
        }

        public void AddNotExistEvent(INotification notification)
        {
            if(!Events.Contains(notification)) Events.Add(notification);
        }

        public List<INotification> GetAllEvents()
        {
            return Events;
        }

        public void RemoveEvent()
        {
            Events.Clear();
        }
    }
}
