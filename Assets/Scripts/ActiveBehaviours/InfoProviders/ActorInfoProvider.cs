using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public class ActorInfoProvider : IActorCenterProvider, IActorRbProvider, IActorHeightProvider, IActorTransformProvider, IActorColliderProvider
    {
        public ActorInfoProvider(Transform actorTransform, Rigidbody2D rb, Collider2D actorCollider)
        {
            ActorTransform = actorTransform;
            Rb = rb;
            ActorCollider = actorCollider;
        }
        
        protected readonly Transform ActorTransform;
        protected readonly Rigidbody2D Rb;
        protected readonly Collider2D ActorCollider;

        public Vector3 GetCenter()
        {
            return ActorCollider.bounds.center;
        }

        public Rigidbody2D GetRb()
        {
            return Rb;
        }

        public float GetHeight()
        {
            return ActorCollider.bounds.size.y;
        }

        public Transform GetTransform()
        {
            return ActorTransform;
        }

        public Collider2D GetCollider()
        {
            return ActorCollider;
        }
    }
}