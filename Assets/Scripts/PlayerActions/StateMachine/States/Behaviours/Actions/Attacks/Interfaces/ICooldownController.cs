namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public interface ICooldownController
    {
        public bool CooldownPassed { get; }

        public void StartCooldown();
    }
}