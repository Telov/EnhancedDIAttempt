using System;
using Telov.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class InputBasedMoveRuler : IMoveRuler
    {
        public InputBasedMoveRuler
        (
            IUpdatesController updatesController,
            IMoveAllower moveAllower,
            InputAction inputAction
        )
        {
            _updatesController = updatesController;
            _moveAllower = moveAllower;
            _inputAction = inputAction;
        }
        
        private readonly IUpdatesController _updatesController;
        private readonly IMoveAllower _moveAllower;
        private readonly InputAction _inputAction;

        private int _listenersCount;

        public event Action<Vector3> OnWantMove
        {
            add
            {
                _innerOnWantMove += value;
                _listenersCount++;
                if (_listenersCount == 1) Activate();
            }
            remove
            {
                _innerOnWantMove -= value;
                _listenersCount--;
                if (_listenersCount == 0) Deactivate();
            }
        }

        private Action<Vector3> _innerOnWantMove = (_) => { };

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
            _updatesController.FixedUpdateCallbacks += OnFixedUpdate;
        }

        private void StopWorking(InputAction.CallbackContext ctx)
        {
            _updatesController.FixedUpdateCallbacks -= OnFixedUpdate;
        }

        private void OnFixedUpdate()
        {
            if (!_moveAllower.Allows) return;
            _innerOnWantMove(_inputAction.ReadValue<float>() * Vector3.right);
        }
    }
}