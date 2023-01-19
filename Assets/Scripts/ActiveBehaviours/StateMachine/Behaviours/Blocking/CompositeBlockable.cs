using System;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class CompositeBlockable : IBlockable
    {
        public CompositeBlockable(params IBlockable[] blockables)
        {
            _blockables = blockables;
        }
        
        private readonly IBlockable[] _blockables;
        
        public event Action OnBlocked = () => { };
        public event Action OnUnblocked = () => { };

        public void Block(object blocker)
        {
            Blocked = true;
            
            foreach (var blockable in _blockables)
            {
                blockable.Block(blocker);
            }
            
            OnBlocked.Invoke();
        }

        public void Unblock(object blocker)
        {
            Blocked = false;
            
            foreach (var blockable in _blockables)
            {
                blockable.Unblock(blocker);
            }
            
            OnUnblocked.Invoke();
        }

        public bool Blocked { get; private set; }
    }
}