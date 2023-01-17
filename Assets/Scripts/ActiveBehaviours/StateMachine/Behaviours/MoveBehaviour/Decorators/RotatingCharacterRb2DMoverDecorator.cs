using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class RotatingCharacterRb2DMoverDecorator : IRb2DMover
    {
        public RotatingCharacterRb2DMoverDecorator(IRb2DMover rb2DMover, Transform transform)
        {
            _rb2DMover = rb2DMover;
            _transform = transform;
        }

        private readonly IRb2DMover _rb2DMover;
        private readonly Transform _transform;

        private bool _rotated = false;

        public void Move(float speedMultiplier, Vector3 direction)
        {
            if (direction.x > 0 == _rotated)
            {
                _transform.Rotate(Vector3.up, 180f);
                _rotated = !_rotated;
            }
            
            _rb2DMover.Move(speedMultiplier, direction);
        }
    }
}