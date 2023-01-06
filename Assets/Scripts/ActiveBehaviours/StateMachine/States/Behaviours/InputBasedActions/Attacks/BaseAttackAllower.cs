namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public class BaseAttackAllower : IAttackAllower
    {
        public BaseAttackAllower(float maxAttackDuration)
        {
            _maxAttackDuration = maxAttackDuration;
        }

        private readonly float _maxAttackDuration;
        
        public bool AttackContinues(float timeSinceAttackStart)
        {
            return timeSinceAttackStart < _maxAttackDuration;
        }
    }
}