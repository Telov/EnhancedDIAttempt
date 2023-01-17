using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class Context
    {
        public event Action OnActivated = () => { };
        public event Action OnDeactivated = () => { };

        public void InvokeDeactivated()
        {
            OnDeactivated.Invoke();
        }
    }
}