using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public interface IContinuousDamageDealer : IDamageDealer
    {
        public event Action OnAttackStarted;
        public event Action OnAttackEnded;
    }
}