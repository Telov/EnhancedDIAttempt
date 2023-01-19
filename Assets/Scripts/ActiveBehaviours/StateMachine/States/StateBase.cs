namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class StateBase : IState
    {
        public StateBase(IBehaviour behaviour)
        {
            _behaviour = behaviour;
        }
    
        private readonly IBehaviour _behaviour;

        public virtual bool IsSuitedToBeAppliedNow()
        {
            return true;
        }

        public virtual void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _behaviour.Activate(callbackContext);
        }
    
        public virtual void Deactivate()
        {
            _behaviour.Deactivate();
        }
    }
}
