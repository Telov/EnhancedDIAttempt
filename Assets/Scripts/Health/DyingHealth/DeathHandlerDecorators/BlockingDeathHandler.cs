using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;

namespace EnhancedDIAttempt.Health
{
    public class BlockingDeathHandler : IDeathHandler
    {
        public BlockingDeathHandler(IDeathHandler deathHandler, params IBlockable[] blockables)
        {
            _deathHandler = deathHandler;
            _blockables = blockables;
        }

        private readonly IDeathHandler _deathHandler;
        private readonly IBlockable[] _blockables;
        
        public void Trigger()
        {
            foreach (var blockable in _blockables)
            {
                blockable.Block(this);
            }

            _deathHandler.Trigger();
        }
    }
}