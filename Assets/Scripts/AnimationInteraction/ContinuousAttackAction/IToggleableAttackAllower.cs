using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public interface IToggleableAttackAllower : IAttackAllower
    {
        public void ToggleOff();
        public void ToggleOn();
    }
}