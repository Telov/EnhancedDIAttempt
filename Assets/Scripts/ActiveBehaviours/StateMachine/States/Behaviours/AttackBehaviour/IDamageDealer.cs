namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public interface IDamageDealer
    {
        public void DealDamage(IAttackTargetsProvider targetsProvider, float amount);
    }
}