using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IBlockable
    {
        public event Action OnBlocked;
        public event Action OnUnblocked;
        public void Block(object blocker);
        public void Unblock(object blocker);
        public bool Blocked { get; }
    }
}