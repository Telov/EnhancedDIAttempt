namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IReloader
    {
        public bool Reloaded { get; }

        public void Unload();
    }
}