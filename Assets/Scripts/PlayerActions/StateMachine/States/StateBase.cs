using System.Collections.Generic;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States
{
    public class StateBase : IState
    {
        public StateBase(List<IBehaviour> behaviours)
        {
            _behaviours = behaviours;
        }
    
        private readonly List<IBehaviour> _behaviours;

        public virtual bool IsSuitedToBeAppliedNow()
        {
            return true;
        }

        public virtual void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            foreach (IBehaviour behaviour in _behaviours)
            {
                behaviour.Activate(callbackContext);
            }
        }
    
        public virtual void Deactivate()
        {
            foreach (IBehaviour behaviour in _behaviours)
            {
                behaviour.Deactivate();
            }
        }
    }
}
