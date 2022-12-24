using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public class Rb2DMover : IRb2DMover
    {
        public Rb2DMover(Rigidbody2D rb)
        {
            _rb = rb;
        }

        private readonly Rigidbody2D _rb;
        
        public void Move(float forceMultiplier, Vector3 direction)
        {
            Vector3 force = direction.normalized * forceMultiplier * 10f;
            _rb.AddForce(force, ForceMode2D.Force);
        }
    }
}