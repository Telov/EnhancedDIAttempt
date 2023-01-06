using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public interface IToggleableAttackAllower : IAttackAllower
    {
        public void ToggleOff();
        public void ToggleOn();
    }
}