using System.Collections.Generic;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class SimpleBehavioursProvider : IBehavioursProvider
    {
        public SimpleBehavioursProvider(ICollection<IBehaviour> behaviours)
        {
            foreach (var behaviour in behaviours)
            {
                _behaviours.Add(behaviour);
            }
        }

        private readonly List<IBehaviour> _behaviours = new ();

        public List<IBehaviour> GetBehaviours()
        {
            return _behaviours;
        }
    }
}