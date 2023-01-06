using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public interface IContinuousDamageDealer : IDamageDealer
    {
        public event Action OnAttackStarted;
        public event Action OnAttackEnded;
    }
}