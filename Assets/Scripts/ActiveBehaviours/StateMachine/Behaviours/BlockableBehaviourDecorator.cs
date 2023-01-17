using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class BlockableBehaviourDecorator : IBehaviour
    {
        public BlockableBehaviourDecorator(IBehaviour behaviour, IBlocker blocker)
        {
            _behaviour = behaviour;
            _blocker = blocker;
        }

        private readonly IBehaviour _behaviour;
        private readonly IBlocker _blocker;

        private bool _haveDeactivated = true;
        private bool _firstActivation = true;

        private EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext _lastContext;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            if (_firstActivation)
            {
                _firstActivation = false;
                _blocker.OnBlocked += Deactivate;
                _blocker.OnUnblocked += () => Activate(_lastContext);
            }

            _lastContext = callbackContext;
            if (!_blocker.Blocked)
            {
                _behaviour.Activate(callbackContext);
                _haveDeactivated = false;
            }
        }

        public void Deactivate()
        {
            if (_haveDeactivated == false)
            {
                _haveDeactivated = true;
                _behaviour.Deactivate();
            }
        }
    }
}