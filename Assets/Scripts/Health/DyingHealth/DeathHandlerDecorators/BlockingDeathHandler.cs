using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;

namespace EnhancedDIAttempt.Health
{
    public class BlockingDeathHandler : IDeathHandler
    {
        public BlockingDeathHandler(IDeathHandler deathHandler, IBlocker blocker)
        {
            _deathHandler = deathHandler;
            _blocker = blocker;
        }

        private readonly IDeathHandler _deathHandler;
        private readonly IBlocker _blocker;
        
        public void Trigger()
        {
            _blocker.Block();

            _deathHandler.Trigger();
        }
    }
}