namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public interface IDamageDealer
    {
        public void DealDamage(IAttackTargetsProvider targetsProvider, float amount);
    }
}