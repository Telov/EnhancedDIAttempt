using UnityEngine;

namespace EnhancedDIAttempt.PlayerActions
{
    public interface IPlayerTransformProvider
    {
        public Transform GetTransform();
    }
}