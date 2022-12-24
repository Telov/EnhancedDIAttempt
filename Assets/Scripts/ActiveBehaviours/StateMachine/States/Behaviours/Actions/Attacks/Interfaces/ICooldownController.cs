namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public interface ICooldownController
    {
        public bool CooldownPassed { get; }

        public void StartCooldown();
    }
}