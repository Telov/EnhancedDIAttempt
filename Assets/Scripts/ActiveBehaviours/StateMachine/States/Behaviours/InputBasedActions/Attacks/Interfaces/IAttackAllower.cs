namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public interface IAttackAllower
    {
        public bool AttackContinues(float timeSinceAttackStart);
    }
}