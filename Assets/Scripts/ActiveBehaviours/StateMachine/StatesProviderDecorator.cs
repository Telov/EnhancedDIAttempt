using System.Collections.Generic;
using System.Linq;
using EnhancedDIAttempt.StateMachine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine
{
    public class StatesProviderDecorator : IStatesProvider
    {
        public StatesProviderDecorator(IStatesProvider statesProvider, params IState[] states)
        {
            _statesProvider = statesProvider;
            _states = states;
        }

        private readonly IStatesProvider _statesProvider;
        private readonly IState[] _states;

        public List<IState> GetStates()
        {
            return new List<IState>(_statesProvider.GetStates().Concat(_states));
        }
    }
}