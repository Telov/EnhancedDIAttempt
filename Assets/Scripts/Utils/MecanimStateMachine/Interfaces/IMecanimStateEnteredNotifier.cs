using System;

namespace EnhancedDIAttempt.Utils.MecanimStateMachine
{
    public interface IMecanimStateEnteredNotifier
    {
        public event Action OnStateEntered;
    }
}