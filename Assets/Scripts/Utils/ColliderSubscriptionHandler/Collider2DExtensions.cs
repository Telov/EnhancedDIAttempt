using System;
using System.Collections.Generic;
using UnityEngine;

namespace EnhancedDIAttempt.Utils.CollisionManager
{
    public static class Collider2DExtensions
    {
        public static ICollisionManager GetCollisionManager(this Collider2D collider)
        {
            return collider.GetComponent<ICollisionManager>();
        }
        
        public static IEnumerable<ICollisionManager> GetCollisionManagerFromEachCollider(this IEnumerable<Collider2D> colliders)
        {
            foreach (Collider2D collider in colliders)
            {
                var manager = collider.GetCollisionManager();
                if(manager == null) continue;
                yield return manager;
            }
        }

        public static IEnumerable<T> GetAllTFromEachCollisionManager<T>(this IEnumerable<Collider2D> colliders)
        {
            foreach (Collider2D collider in colliders)
            {
                var manager = collider.GetCollisionManager();
                if(manager == null) continue;
                foreach (T subscriber in manager.GetSubscribers<T>())
                {
                    yield return subscriber;
                }
            }
        }

        public static void CallWithEachTOfEachCollisionManager<T>(this IEnumerable<Collider2D> colliders, Action<T> action)
        {
            foreach (Collider2D collider in colliders)
            {
                var manager = collider.GetCollisionManager();
                if(manager == null) continue;
                manager.CallForEachT<T>(action);
            }
        }
    }
}