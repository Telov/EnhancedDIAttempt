using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public interface IRb2DMover
    {
        public void Move(float speedMultiplier, Vector3 direction);
    }
}
