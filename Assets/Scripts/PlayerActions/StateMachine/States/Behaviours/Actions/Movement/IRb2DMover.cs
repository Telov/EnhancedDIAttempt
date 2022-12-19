using UnityEngine;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public interface IRb2DMover
    {
        public void Move(float forceMultiplier, Vector3 direction);
    }
}