namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public interface IDamageDealer
    {
        public void DealDamage(IAttackTargetsProvider targetsProvider, float amount);
    }
}