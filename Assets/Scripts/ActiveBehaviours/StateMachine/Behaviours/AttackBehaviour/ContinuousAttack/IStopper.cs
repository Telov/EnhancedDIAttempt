using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IStopper
    {
        public event Action OnStopped;
        public void Stop();
    }
}