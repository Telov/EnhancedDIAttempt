using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public interface IPlayerRbProvider
    {
        public Rigidbody2D GetRb();
    }
}