using System;
using System.Collections.Generic;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class SimpleBlocker : IBlocker
    {
        private readonly List<object> _blockers = new();
        public void Block(object blocker)
        {
            if (blocker == null) throw new Exception("Trying to block with null object");
            
            _blockers.Add(blocker);
            if (_blockers.Count == 1)
            {
                Blocked = true;
                OnBlocked();
            }
        }

        public void Unblock(object blocker)
        {
            if (blocker == null) throw new Exception("Trying to unblock with null object");

            if (!_blockers.Contains(blocker)) throw new Exception("Trying to unblock with object which isn't blocking");

            _blockers.Remove(blocker);
            if (_blockers.Count == 0)
            {
                Blocked = false;
                OnUnblocked();
            }
        }

        public event Action OnBlocked = () => { };
        public event Action OnUnblocked = () => { };
        public bool Blocked { get; private set; }
    }
}