using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public interface IContinuousDamageDealer : IDamageDealer
    {
        public event Action OnAttackStarted;
        public event Action OnAttackEnded;
    }
}