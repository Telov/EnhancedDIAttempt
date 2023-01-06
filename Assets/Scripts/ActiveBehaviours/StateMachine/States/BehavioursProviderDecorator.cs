using System.Collections.Generic;
using System.Linq;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class BehavioursProviderDecorator : IBehavioursProvider
    {
        public BehavioursProviderDecorator(IBehavioursProvider behavioursProvider, params IBehaviour[] behaviours)
        {
            _behavioursProvider = behavioursProvider;
            _behaviours = behaviours;
        }

        private readonly IBehavioursProvider _behavioursProvider;
        private readonly IBehaviour[] _behaviours;
        
        public List<IBehaviour> GetBehaviours()
        {
            return new List<IBehaviour>(_behavioursProvider.GetBehaviours().Concat(_behaviours));
        }
    }
}