using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class SimpleJumper : IJumper
    {
        public SimpleJumper(Transform transform, Rigidbody2D rb)
        {
            _transform = transform;
            _rb = rb;
        }

        private readonly Transform _transform;
        private readonly Rigidbody2D _rb;
        
        public void Jump(float jumpPower)
        {
            _rb.AddForce(_transform.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}