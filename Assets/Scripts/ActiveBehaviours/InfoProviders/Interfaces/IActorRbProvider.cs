using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public interface IActorRbProvider
    {
        public Rigidbody2D GetRb();
    }
}