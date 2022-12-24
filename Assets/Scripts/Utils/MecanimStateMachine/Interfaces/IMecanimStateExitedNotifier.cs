using System;

namespace EnhancedDIAttempt.Utils.MecanimStateMachine
{
    public interface IMecanimStateExitedNotifier
    {
        public event Action OnStateExited;
    }
}