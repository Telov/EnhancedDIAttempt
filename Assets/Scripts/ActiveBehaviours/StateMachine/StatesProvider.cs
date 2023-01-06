using System.Collections.Generic;
using EnhancedDIAttempt.StateMachine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine
{
    public class StatesProvider : IStatesProvider
    {
        public StatesProvider(ICollection<IState> states)
        {
            _states = new List<IState>(states);
        }

        private readonly List<IState> _states;
        public List<IState> GetStates()
        {
            return _states;
        }
    }
}