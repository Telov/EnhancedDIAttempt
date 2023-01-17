using System.Collections.Generic;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine
{
    public interface IBehavioursProvider
    {
        public List<IBehaviour> GetBehaviours();
    }
}