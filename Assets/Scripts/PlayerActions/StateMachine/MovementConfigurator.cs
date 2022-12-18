using System.Collections.Generic;
using EnhancedDIAttempt.StateMachine;

namespace EnhancedDIAttempt.PlayerActions.StateMachine
{
    public class MovementConfigurator : ISpareStatesProvider
    {
        public MovementConfigurator(params IState[] states)
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