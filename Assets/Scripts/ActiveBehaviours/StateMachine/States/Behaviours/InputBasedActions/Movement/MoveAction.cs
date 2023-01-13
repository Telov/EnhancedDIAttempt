using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
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
            _data = data;
        }

        private readonly MovementSettings _movementSettings;
        private readonly MoveActionData _data;

        private bool _started;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            if (_data.InputAction.inProgress) TryStartWorking(default);
            _data.InputAction.started += TryStartWorking;
            _data.InputAction.canceled += StopWorking;
        }

        public void Deactivate()
        {
            if (_started) StopWorking(default);
            _data.InputAction.started -= TryStartWorking;
            _data.InputAction.canceled -= StopWorking;
        }

        private void TryStartWorking(InputAction.CallbackContext ctx)
        {
            if (!_started)
            {
                _started = true;
                _data.UpdatesController.AddFixedUpdateCallback(OnFixedUpdate);
            }
        }

        private void StopWorking(InputAction.CallbackContext ctx)
        {
            _started = false;
            _data.UpdatesController.RemoveFixedUpdateCallback(OnFixedUpdate);
        }

        private void OnFixedUpdate()
        {
            if(!_data.MoveAllower.Allows) return;
            _data.Rb2DMover.Move(_movementSettings.WalkSpeed, _data.InputAction.ReadValue<float>() * Vector3.right);
        }

        [System.Serializable]
        public class MovementSettings
        {
            public float WalkSpeed;
        }
    }
}