namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public interface IAttackAllower
    {
        public bool AttackContinues(float timeSinceAttackStart);
    }
}