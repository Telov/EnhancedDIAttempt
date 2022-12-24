using System.Collections.Generic;
using EnhancedDIAttempt.StateMachine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine
{
    public class ActiveBehaviours : ISpareStatesProvider
    {
        public ActiveBehaviours(params IState[] states)
        {
            _states = new List<IState>(states);
        }

        private readonly List<IState> _states;
        public List<IState> GetSpareStates()
        {
            return _states;
        }
    }
}