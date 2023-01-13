using System;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public class InputBasedMoveRulerDecorator : IMoveRuler
    {
        public InputBasedMoveRulerDecorator
        (
            IMoveRuler moveRuler,
            IUpdatesController updatesController,
            IMoveAllower moveAllower,
            InputAction inputAction,
            MovementSettings movementSettings
        )
        {
            _moveRuler = moveRuler;
            _updatesController = updatesController;
            _moveAllower = moveAllower;
            _inputAction = inputAction;
            _movementSettings = movementSettings;
        }

        private readonly IMoveRuler _moveRuler;
        private readonly IUpdatesController _updatesController;
        private readonly IMoveAllower _moveAllower;
        private readonly InputAction _inputAction;
        private readonly MovementSettings _movementSettings;

        private int _listenersCount;

        public event Action<float, Vector3> OnWantMove
        {
            add
            {
                _innerOnWantMove += value;
                _listenersCount++;
                _moveRuler.OnWantMove += value;
                if (_listenersCount == 1) Activate();
            }
            remove
            {
                _innerOnWantMove -= value;
                _listenersCount--;
                _moveRuler.OnWantMove -= value;
                if (_listenersCount == 0) Deactivate();
            }
        }

        private Action<float, Vector3> _innerOnWantMove = (_, _) => { };

        public void Activate()
        {
            if (_inputAction.inProgress) StartWorking(default);
            _inputAction.started += StartWorking;
            _inputAction.canceled += StopWorking;
        }

        public void Deactivate()
        {
            StopWorking(default);
            _inputAction.started -= StartWorking;
            _inputAction.canceled -= StopWorking;
        }

        private void StartWorking(InputAction.CallbackContext ctx)
        {
            _updatesController.AddFixedUpdateCallback(OnFixedUpdate);
        }

        private void StopWorking(InputAction.CallbackContext ctx)
        {
            _updatesController.RemoveFixedUpdateCallback(OnFixedUpdate);
        }

        private void OnFixedUpdate()
        {
            if (!_moveAllower.Allows) return;
            _innerOnWantMove(_movementSettings.WalkSpeed, _inputAction.ReadValue<float>() * Vector3.right);
        }

        [System.Serializable]
        public class MovementSettings
        {
            public float WalkSpeed;
        }
    }
}