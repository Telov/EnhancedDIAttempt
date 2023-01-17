namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public interface IBlockable
    {
        public void Block(object blocker);
        public void Unblock(object blocker);
    }
}