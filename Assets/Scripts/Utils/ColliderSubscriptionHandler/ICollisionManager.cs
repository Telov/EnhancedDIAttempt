using System;
using System.Collections.Generic;

namespace EnhancedDIAttempt.Utils.CollisionManager
{
    public interface ICollisionManager
    {
        public void Subscribe(object newSubscriber);
        
        public void Unsubscribe(object unsubscriber);

        public List<T> GetSubscribers<T>();

        public void CallForEachT<T>(Action<T> action);
    }
}