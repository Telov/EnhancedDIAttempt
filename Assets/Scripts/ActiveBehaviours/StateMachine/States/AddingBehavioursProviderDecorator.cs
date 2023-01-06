using System.Collections.Generic;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class AddingBehavioursProviderDecorator : IBehavioursProvider
    {
        public AddingBehavioursProviderDecorator(IBehavioursProvider provider, ICollection<IBehaviour> behaviours)
        {
            _behaviours = new List<IBehaviour>(provider.GetBehaviours());
            foreach (var behaviour in behaviours)
            {
                _behaviours.Add(behaviour);
            }
        }

        private readonly List<IBehaviour> _behaviours;

        public List<IBehaviour> GetBehaviours()
        {
            return _behaviours;
        }
    }
}