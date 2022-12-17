using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnhancedDIAttempt.Utils.CollisionManager
{
    public class CollisionManager : MonoBehaviour, ICollisionManager
    {
        private readonly List<object> _list = new List<object>();

        public void Subscribe(object newSubscriber)
        {
            _list.Add(newSubscriber);
        }

        public void Unsubscribe(object unsubscriber)
        {
            _list.Remove(unsubscriber);
        }

        public List<T> GetSubscribers<T>()
        {
            List<T> resultList = new List<T>();
            
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] is T subscriber)
                {
                    resultList.Add(subscriber);
                }
            }

            return resultList;
        }

        public void CallForEachT<T>(Action<T> action)
        {
            foreach (T subscriber in GetSubscribers<T>())
            {
                action(subscriber);
            }
        }
    }
}