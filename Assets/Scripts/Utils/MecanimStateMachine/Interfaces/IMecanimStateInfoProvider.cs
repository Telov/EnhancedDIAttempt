namespace EnhancedDIAttempt.Utils.MecanimStateMachine
{
    public interface IMecanimStateInfoProvider : IMecanimStateExitedNotifier, IMecanimStateEnteredNotifier
    {
        public bool IsActive { get; }
    }
}