using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Telov.Utils
{
    public static class Collider2DExtensions
    {
        public static ICollisionManager GetCollisionManager(this Collider2D collider)
        {
            return collider.GetComponent<ICollisionManager>();
        }
        
        public static ICollection<ICollisionManager> GetCollisionManagerFromEachCollider(this IEnumerable<Collider2D> colliders)
        {
            List<ICollisionManager> list = new();
            foreach (Collider2D collider in colliders)
            {
                var manager = collider.GetCollisionManager();
                if(manager == null) continue;
                list.Add(manager);
            }

            return new List<ICollisionManager>(list);
        }

        public static ICollection<T> GetAllTFromEachCollisionManager<T>(this IEnumerable<Collider2D> colliders)
        {
            List<T> list = new();
            foreach (Collider2D collider in colliders)
            {
                var manager = collider.GetCollisionManager();
                if(manager == null) continue;

                foreach (var subscriber in manager.GetSubscribers<T>())
                {
                    list.Add(subscriber);
                }
            }
            
            return new List<T>(list);
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