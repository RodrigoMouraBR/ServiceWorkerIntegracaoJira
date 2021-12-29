using BeeFor.Core.Notifications;
using System.Collections.Generic;

namespace BeeFor.Core.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}