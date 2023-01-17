using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IRb2DMover
    {
        public void Move(float speedMultiplier, Vector3 direction);
    }
}
