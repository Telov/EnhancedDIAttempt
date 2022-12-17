using UnityEngine;

namespace EnhancedDIAttempt.PlayerActions
{
    public interface IPlayerRbProvider
    {
        public Rigidbody2D GetRb();
    }
}