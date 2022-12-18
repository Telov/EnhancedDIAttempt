namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public interface IAttackAllower
    {
        public bool AttackContinues(float timeSinceAttackStart);
    }
}