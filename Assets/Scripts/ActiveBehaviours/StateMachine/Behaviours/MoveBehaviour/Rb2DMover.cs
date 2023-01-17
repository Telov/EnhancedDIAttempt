using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class Rb2DMover : IRb2DMover
    {
        public Rb2DMover(Rigidbody2D rb)
        {
            _rb = rb;
        }

        private readonly Rigidbody2D _rb;
        
        public void Move(float speedMultiplier, Vector3 direction)
        {
            Vector3 force = direction.normalized * speedMultiplier * 10f;
            _rb.AddForce(force, ForceMode2D.Force);
        }
    }
}