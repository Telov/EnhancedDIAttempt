using System.Collections.Generic;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public interface IBehavioursProvider
    {
        public List<IBehaviour> GetBehaviours();
    }
}