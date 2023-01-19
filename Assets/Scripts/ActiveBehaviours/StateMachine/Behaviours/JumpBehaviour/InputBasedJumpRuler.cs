using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class InputBasedJumpRuler : IJumpRuler
    {
        public InputBasedJumpRuler(InputAction jumpAction)
        {
            _jumpAction = jumpAction;
        }
        
        private readonly InputAction _jumpAction;

        private int _listenersCount;
        
        public event Action OnWantToJump
        {
            add
            {
                _innerOnWantToJump += value;
                _listenersCount++;
                if (_listenersCount == 1) _jumpAction.performed += RequestJump;
            }
            remove
            {
                _innerOnWantToJump -= value;
                _listenersCount--;
                if (_listenersCount == 0) _jumpAction.performed -= RequestJump;
            }
        }

        private Action _innerOnWantToJump = () => { };

        private void RequestJump(InputAction.CallbackContext ctx)
        {
            _innerOnWantToJump();
        }
    }
}