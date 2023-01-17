using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IBlocker : IBlockable
    {
        public event Action OnBlocked;
        public event Action OnUnblocked;
        public bool Blocked { get; }
    }
}