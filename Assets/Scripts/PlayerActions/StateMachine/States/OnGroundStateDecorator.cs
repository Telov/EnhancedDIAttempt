using UnityEngine;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States
{
    public class OnGroundStateDecorator : IState
    {
        public OnGroundStateDecorator(
            IState state,
            IGroundChecker groundChecker,
            IUpdatesController updatesController,
            Rigidbody2D rb,
            float horizontalVelSlowdownCoef
        )
        {
            _state = state;
            _groundChecker = groundChecker;
            _updatesController = updatesController;
            _rb = rb;
            _horizontalVelSlowdownCoef = horizontalVelSlowdownCoef;
        }

        private readonly IState _state;
        private readonly IGroundChecker _groundChecker;
        private readonly IUpdatesController _updatesController;
        private readonly Rigidbody2D _rb;
        private readonly float _horizontalVelSlowdownCoef;

        private EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext _callbackContext;

        public bool IsSuitedToBeAppliedNow()
        {
            return _groundChecker.IsPlayerGrounded() && _state.IsSuitedToBeAppliedNow();
        }

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _callbackContext = callbackContext;
            _state.Activate(callbackContext);
            StartUpdating();
        }

        public void Deactivate()
        {
            _state.Deactivate();
            StopUpdating();
        }


        private void StartUpdating()
        {
            _updatesController.AddUpdateCallback(OnUpdate);
            _updatesController.AddUpdateCallback(OnFixedUpdate);
        }

        private void StopUpdating()
        {
            _updatesController.RemoveUpdateCallback(OnUpdate);
            _updatesController.RemoveUpdateCallback(OnFixedUpdate);
        }

        private void OnUpdate()
        {
            if (!_groundChecker.IsPlayerGrounded()) _callbackContext.ExitCurrentState();
        }

        private void OnFixedUpdate()
        {
            if (Mathf.Abs(_rb.velocity.x) > 0.0001f)
            {
                var velocity = _rb.velocity;
                _rb.velocity = new Vector2(velocity.x * (1 - _horizontalVelSlowdownCoef), velocity.y);
                if (Mathf.Abs(_rb.velocity.x) < 0.0001f) _rb.velocity = new Vector2(0f, velocity.y);
            }
        }
    }
}