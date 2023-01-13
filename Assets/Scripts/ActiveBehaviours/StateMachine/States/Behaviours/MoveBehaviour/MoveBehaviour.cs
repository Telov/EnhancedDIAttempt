using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Interfaces;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class MoveBehaviour : IBehaviour
    {
        public MoveBehaviour(IRb2DMover mover, IMoveRuler moveRuler)
        {
            _mover = mover;
            _moveRuler = moveRuler;
        }

        private readonly IRb2DMover _mover;
        private readonly IMoveRuler _moveRuler;
        
        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _moveRuler.OnWantMove += _mover.Move;
        }

        public void Deactivate()
        {
            _moveRuler.OnWantMove -= _mover.Move;
        }
    }
}