using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public class PlayerInfoProvider : IPlayerCenterProvider, IPlayerRbProvider, IPlayerHeightProvider, IPlayerTransformProvider
    {
        public PlayerInfoProvider(Transform playerTransform, Rigidbody2D rb, Collider2D playerCollider)
        {
            PlayerTransform = playerTransform;
            Rb = rb;
            PlayerCollider = playerCollider;
        }
        
        protected readonly Transform PlayerTransform;
        protected readonly Rigidbody2D Rb;
        protected readonly Collider2D PlayerCollider;

        public Vector3 GetCenter()
        {
            return PlayerCollider.bounds.center;
        }

        public Rigidbody2D GetRb()
        {
            return Rb;
        }

        public float GetHeight()
        {
            return PlayerCollider.bounds.size.y;
        }

        public Transform GetTransform()
        {
            return PlayerTransform;
        }
    }
}