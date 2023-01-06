using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class ToggleableAttackAllowerDecorator : IToggleableAttackAllower
    {
        public ToggleableAttackAllowerDecorator(IAttackAllower attackAllower)
        {
            _attackAllower = attackAllower;
        }

        private readonly IAttackAllower _attackAllower;

        private bool _toggledOn = false;
        
        public bool AttackContinues(float timeSinceAttackStart)
        {
            return _attackAllower.AttackContinues(timeSinceAttackStart) && _toggledOn;
        }

        public void ToggleOff()
        {
            _toggledOn = false;
        }

        public void ToggleOn()
        {
            _toggledOn = true;
        }
    }
}