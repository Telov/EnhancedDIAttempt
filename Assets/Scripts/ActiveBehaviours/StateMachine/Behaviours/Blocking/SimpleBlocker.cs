namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class SimpleBlocker : IBlocker
    {
        public SimpleBlocker(IBlockable blockable)
        {
            _blockable = blockable;
        }
        
        private readonly IBlockable _blockable;
        
        public void Block()
        {
            _blockable.Block(this);
        }

        public void Unblock()
        {
            _blockable.Unblock(this);
        }
    }
}