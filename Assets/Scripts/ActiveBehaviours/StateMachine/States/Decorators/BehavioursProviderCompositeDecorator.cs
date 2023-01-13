using System.Collections.Generic;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class BehavioursProviderCompositeDecorator : IBehavioursProvider
    {
        public BehavioursProviderCompositeDecorator(IBehavioursProvider behavioursProvider, params IBehaviour[] behaviours)
        {
            _behaviours = new List<IBehaviour>(behavioursProvider.GetBehaviours());
            foreach (var behaviour in behaviours)
            {
                _behaviours.Add(behaviour);
            }
        }
        
        private readonly List<IBehaviour> _behaviours;
        
        public List<IBehaviour> GetBehaviours()
        {
            return new List<IBehaviour>(_behaviours);
        }
    }
}