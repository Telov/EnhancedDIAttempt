using System.Collections.Generic;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine
{
    public class SimpleBehavioursProvider : IBehavioursProvider
    {
        public SimpleBehavioursProvider(params IBehaviour[] behaviours)
        {
            _behaviours  = new List<IBehaviour>(behaviours);
        }

        private readonly List<IBehaviour> _behaviours;

        public List<IBehaviour> GetBehaviours()
        {
            return _behaviours;
        }
    }
}