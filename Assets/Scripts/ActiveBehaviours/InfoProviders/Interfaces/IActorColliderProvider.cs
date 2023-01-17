using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public interface IActorColliderProvider
    {
        public Collider2D GetCollider();
    }
}