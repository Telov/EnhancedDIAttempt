namespace Telov.Utils
{
    public interface IMecanimStateInfoProvider : IMecanimStateExitedNotifier, IMecanimStateEnteredNotifier
    {
        public bool IsActive { get; }
    }
}