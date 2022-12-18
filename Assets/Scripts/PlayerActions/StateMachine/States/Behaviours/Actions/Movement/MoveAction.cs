using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class MoveAction : IBehaviour
    {
        public MoveAction
        (
            MovementSettings movementSettings,
            MoveActionData data
        )
        {
            _movementSettings = movementSettings;
            _rb = data.PlayerRbProvider.GetRb();
            _groundChecker = data.GroundChecker;
            _updatesController = data.UpdatesController;
            _moveAction = data.MoveAction;
        }

        private readonly MovementSettings _movementSettings;
        private readonly Rigidbody2D _rb;
        private readonly IGroundChecker _groundChecker;
        private readonly IUpdatesController _updatesController;
        private readonly InputAction _moveAction;

        private bool _started;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            if (_moveAction.inProgress) TryStartWorking(default);
            _moveAction.started += TryStartWorking;
            _moveAction.canceled += StopWorking;
        }

        public void Deactivate()
        {
            if (_started) StopWorking(default);
            _moveAction.started -= TryStartWorking;
            _moveAction.canceled -= StopWorking;
        }

        private void TryStartWorking(InputAction.CallbackContext ctx)
        {
            if (!_started)
            {
                _started = true;
                _updatesController.AddUpdateCallback(OnUpdate);
                _updatesController.AddFixedUpdateCallback(OnFixedUpdate);
            }
        }

        private void StopWorking(InputAction.CallbackContext ctx)
        {
            _started = false;
            _updatesController.RemoveUpdateCallback(OnUpdate);
            _updatesController.RemoveFixedUpdateCallback(OnFixedUpdate);
        }

        private void OnUpdate()
        {
            LimitHorizontalSpeed(_movementSettings.WalkSpeed);

            // handle drag
            _rb.drag = _groundChecker.IsPlayerGrounded() ? _movementSettings.GroundDrag : 0;
        }

        private void OnFixedUpdate()
        {
            MovePlayer();
        }

        private void LimitHorizontalSpeed(float limit)
        {
            var velocity = _rb.velocity;

            if (Mathf.Abs(velocity.x) > limit)
            {
                _rb.velocity = new Vector2(Mathf.Clamp(velocity.x, -limit, limit), velocity.y);
            }
        }


        private void MovePlayer()
        {
            float moveInput = _moveAction.ReadValue<float>();

            Vector3 force = Vector2.right * moveInput * _movementSettings.WalkSpeed * 10f;
            _rb.AddForce(force, ForceMode2D.Force);
        }

        [System.Serializable]
        public class MovementSettings
        {
            public float WalkSpeed;
            public float GroundDrag;
        }
    }
}