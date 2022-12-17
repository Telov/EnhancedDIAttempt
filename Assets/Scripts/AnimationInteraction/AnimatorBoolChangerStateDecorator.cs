using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class AnimatorBoolChangerStateDecorator : IState
    {
        public AnimatorBoolChangerStateDecorator(IState state, Animator animator, int boolId, bool reversedInteractionWithTheBool)
        {
            _state = state;
            _animator = animator;
            _boolId = boolId;
            _reversedInteractionWithTheBool = reversedInteractionWithTheBool;
        }

        private readonly IState _state;
        private readonly Animator _animator;
        private readonly int _boolId;
        private readonly bool _reversedInteractionWithTheBool; 
        
        public bool IsSuitedToBeAppliedNow()
        {
            return _state.IsSuitedToBeAppliedNow();
        }

        public void Activate(StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _state.Activate(callbackContext);
            _animator.SetBool(_boolId, true && !_reversedInteractionWithTheBool);
        }

        public void Deactivate()
        {
            _state.Deactivate();
            _animator.SetBool(_boolId, false || _reversedInteractionWithTheBool);
        }
    }
}