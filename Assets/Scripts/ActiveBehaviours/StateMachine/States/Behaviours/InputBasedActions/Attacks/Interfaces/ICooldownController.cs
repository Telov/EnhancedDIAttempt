namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public interface ICooldownController
    {
        public bool CooldownPassed { get; }

        public void StartCooldown();
    }
}