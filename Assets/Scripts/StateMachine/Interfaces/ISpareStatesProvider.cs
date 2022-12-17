using System.Collections.Generic;

namespace EnhancedDIAttempt.StateMachine
{
    public interface ISpareStatesProvider
    {
        public List<IState> GetSpareStates();
    }
}