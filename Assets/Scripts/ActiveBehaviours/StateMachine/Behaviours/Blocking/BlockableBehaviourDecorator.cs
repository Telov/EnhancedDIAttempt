namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class BlockableBehaviourDecorator : IBehaviour
    {
        public BlockableBehaviourDecorator(IBehaviour behaviour, IBlockable blockable)
        {
            _behaviour = behaviour;
            _blockable = blockable;
        }

        private readonly IBehaviour _behaviour;
        private readonly IBlockable _blockable;

        private bool _active;
        private bool _firstActivation = true;

        private EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext _lastContext;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            if (_firstActivation)
            {
                _firstActivation = false;
                _blockable.OnBlocked += Deactivate;
                _blockable.OnUnblocked += () => Activate(_lastContext);
            }

            _lastContext = callbackContext;
            if (!_blockable.Blocked)
            {
                _behaviour.Activate(callbackContext);
                _active = true;
            }
        }

        public void Deactivate()
        {
            if (_active)
            {
                _active = false;
                _behaviour.Deactivate();
            }
        }
    }
}