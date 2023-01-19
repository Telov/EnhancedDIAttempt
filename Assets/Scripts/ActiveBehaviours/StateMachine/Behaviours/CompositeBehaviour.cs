namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class CompositeBehaviour : IBehaviour
    {
        public CompositeBehaviour(params IBehaviour[] behaviours)
        {
            _behaviours = behaviours;
        }
        
        private readonly IBehaviour[] _behaviours;
        
        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.Activate(callbackContext);
            }
        }

        public void Deactivate()
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.Deactivate();
            }
        }
    }
}