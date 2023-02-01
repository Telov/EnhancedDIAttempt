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
        private bool _shouldBeActive;
        private bool _firstActivation = true;

        private EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext _lastContext;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            if (_firstActivation)
            {
                _firstActivation = false;
                _blockable.OnBlocked += DeactivateFromBlockable;
                _blockable.OnUnblocked += ActivateFromBlockable;
            }

            _shouldBeActive = true;
            _lastContext = callbackContext;
            if (!_blockable.Blocked)
            {
                InnerActivate();
            }
        }

        public void Deactivate()
        {
            _shouldBeActive = false;
            if (_active)
            {
                InnerDeactivate();
            }
        }
        
        private void ActivateFromBlockable()
        {
            if (_shouldBeActive && !_active)
            {
                InnerActivate();
            }
        }


        private void DeactivateFromBlockable()
        {
            if (_active)
            {
                InnerDeactivate();
            }
        }

        private void InnerActivate()
        {
            _behaviour.Activate(_lastContext);
            _active = true;
        }

        private void InnerDeactivate()
        {
            _active = false;
            _behaviour.Deactivate();
        }
    }
}