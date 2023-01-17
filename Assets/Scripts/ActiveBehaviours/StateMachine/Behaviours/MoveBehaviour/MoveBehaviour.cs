using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class MoveBehaviour : IBehaviour
    {
        public MoveBehaviour(IRb2DMover mover, IMoveRuler moveRuler, float speed)
        {
            _mover = mover;
            _moveRuler = moveRuler;
            _speed = speed;
        }

        private readonly IRb2DMover _mover;
        private readonly IMoveRuler _moveRuler;
        private readonly float _speed;
        
        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _moveRuler.OnWantMove += Move;
        }

        public void Deactivate()
        {
            _moveRuler.OnWantMove -= Move;
        }

        private void Move(Vector3 direction)
        {
            _mover.Move(_speed, direction);
        }
    }
}