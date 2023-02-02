using System;
using System.Collections.Generic;

namespace Telov.Utils
{
    public interface ICollisionManager
    {
        public void SubscribeAsListener(object newSubscriber);
        
        public void UnsubscribeAsListener(object unsubscriber);

        public List<T> GetSubscribers<T>();

        public void CallForEachT<T>(Action<T> action);
    }
}