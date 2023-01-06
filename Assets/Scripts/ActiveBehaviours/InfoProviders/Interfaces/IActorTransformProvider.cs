using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public interface IActorTransformProvider
    {
        public Transform GetTransform();
    }
}