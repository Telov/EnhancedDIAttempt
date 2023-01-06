using System.Collections.Generic;

namespace EnhancedDIAttempt.StateMachine
{
    public interface IStatesProvider
    {
        public List<IState> GetStates();
    }
}