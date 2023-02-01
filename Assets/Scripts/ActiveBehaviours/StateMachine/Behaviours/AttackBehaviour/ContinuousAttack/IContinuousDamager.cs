using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IContinuousDamager : IDamager
    {
        public event Action OnDamagingStarted;
        public event Action OnDamagingEnded;
    }
}