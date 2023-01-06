using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
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

        public void Move(float speedMultiplier, Vector3 direction)
        {
            _transform.RotateAround(Vector3.up, direction.x > 0? 0f : 180f);
            _rb2DMover.Move(speedMultiplier, direction);
        }
    }
}