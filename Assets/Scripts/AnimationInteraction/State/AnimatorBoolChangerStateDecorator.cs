using Telov.Utils;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class AnimatorBoolChangerStateDecorator : IState
    {
        public AnimatorBoolChangerStateDecorator(IState state, IAnimatorBoolSetter animatorBoolSetter, bool reversedInteractionWithTheBool)
        {
            _state = state;
            _animatorBoolSetter = animatorBoolSetter;
            _reversedInteractionWithTheBool = reversedInteractionWithTheBool;
        }

        private readonly IState _state;
        private readonly IAnimatorBoolSetter _animatorBoolSetter;
        private readonly bool _reversedInteractionWithTheBool;

        public bool IsSuitedToBeAppliedNow()
        {
            return _state.IsSuitedToBeAppliedNow();
        }

        public void Activate(StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _state.Activate(callbackContext);
            if (_reversedInteractionWithTheBool) _animatorBoolSetter.SetBoolToFalse();
            else _animatorBoolSetter.SetBoolToTrue();
        }

        public void Deactivate()
        {
            _state.Deactivate();
            if (_reversedInteractionWithTheBool) _animatorBoolSetter.SetBoolToTrue();
            else _animatorBoolSetter.SetBoolToFalse();
        }
    }
}