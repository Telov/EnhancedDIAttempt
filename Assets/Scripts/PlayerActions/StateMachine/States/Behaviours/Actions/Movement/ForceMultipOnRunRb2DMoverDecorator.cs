using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class ForceMultipOnRunRb2DMoverDecorator : IRb2DMover
    {
        public ForceMultipOnRunRb2DMoverDecorator(IRb2DMover rb2DMover, InputAction runAction, float multiplier)
        {
            _runAction = runAction;
            _rb2DMover = rb2DMover;
            _multiplier = multiplier;
        }

        private readonly IRb2DMover _rb2DMover;
        private readonly InputAction _runAction;
        private readonly float _multiplier;
        
        public void Move(float forceMultiplier, Vector3 direction)
        {
            var appliedMultiplier = _runAction.IsPressed() ? _multiplier : 1;
            _rb2DMover.Move(forceMultiplier * appliedMultiplier, direction);
        }
    }
}