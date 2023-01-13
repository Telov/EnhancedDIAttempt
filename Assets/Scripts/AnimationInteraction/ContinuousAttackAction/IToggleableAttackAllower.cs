using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public interface IToggleableAttackAllower : IAttackAllower
    {
        public void ToggleOff();
        public void ToggleOn();
    }
}