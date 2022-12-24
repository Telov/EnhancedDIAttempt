using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public interface IRb2DMover
    {
        public void Move(float forceMultiplier, Vector3 direction);
    }
}