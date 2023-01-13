namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public interface ICooldownController
    {
        public bool CooldownPassed { get; }

        public void StartCooldown();
    }
}