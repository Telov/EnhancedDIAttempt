namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine
{
    public interface IBehaviour
    {
        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext);
        public void Deactivate();
    }
}