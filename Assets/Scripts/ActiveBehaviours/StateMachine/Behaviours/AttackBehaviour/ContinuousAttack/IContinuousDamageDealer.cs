using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IContinuousDamageDealer : IDamageDealer
    {
        public event Action OnAttackStarted;
        public event Action OnAttackEnded;
    }
}