using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class SimpleStopper : IStopper
    {
        public event Action OnStopped = () => { };

        public void Stop()
        {
            OnStopped.Invoke();
        }
    }
}