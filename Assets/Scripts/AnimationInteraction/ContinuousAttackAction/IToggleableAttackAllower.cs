using EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public interface IToggleableAttackAllower : IAttackAllower
    {
        public void ToggleOff();
        public void ToggleOn();
    }
}