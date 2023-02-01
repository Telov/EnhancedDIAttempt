using System.Collections.Generic;
using EnhancedDIAttempt.Health;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class FilteringDamageablesProviderDecorator : IDamageablesProvider
    {
        public FilteringDamageablesProviderDecorator(IDamageablesProvider targetsProvider, ICollection<IDamageable> filter)
        {
            _targetsProvider = targetsProvider;
            _filter = filter;
        }

        private readonly IDamageablesProvider _targetsProvider;
        private readonly ICollection<IDamageable> _filter;
        
        public ICollection<IDamageable> GetAttackTargets()
        {
            var list = new List<IDamageable>(_targetsProvider.GetAttackTargets());
            foreach (var filtered in _filter)
            {
                if (list.Contains(filtered)) list.Remove(filtered);
            }
            return list;
        }
    }
}