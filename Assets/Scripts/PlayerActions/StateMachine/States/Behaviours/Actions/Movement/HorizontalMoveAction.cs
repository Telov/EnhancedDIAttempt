using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class HorizontalMoveAction : IBehaviour
    {
        public HorizontalMoveAction
        (
            MovementSettings movementSettings,
            MoveActionData data
        )
        {
            _movementSettings = movementSettings;
            _updatesController = data.UpdatesController;
            _moveAction = data.MoveAction;
            _rb2DMover = data.Rb2DMover;
        }

        private readonly MovementSettings _movementSettings;
        private readonly IUpdatesController _updatesController;
        private readonly InputAction _moveAction;
        private readonly IRb2DMover _rb2DMover;

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
                _updatesController.AddFixedUpdateCallback(OnFixedUpdate);
            }
        }

        private void StopWorking(InputAction.CallbackContext ctx)
        {
            _started = false;
            _updatesController.RemoveFixedUpdateCallback(OnFixedUpdate);
        }

        private void OnFixedUpdate()
        {
            _rb2DMover.Move(_moveAction.ReadValue<float>() * _movementSettings.WalkSpeed, Vector3.right);
        }

        [System.Serializable]
        public class MovementSettings
        {
            public float WalkSpeed;
        }
    }
}