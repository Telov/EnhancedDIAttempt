using System.Collections.Generic;
using System.Linq;
using EnhancedDIAttempt.Damage;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions
{
    public class FilteringAttackTargetsProviderDecorator : IAttackTargetsProvider
    {
        public FilteringAttackTargetsProviderDecorator(IAttackTargetsProvider targetsProvider, ICollection<IDamageGetter> filter)
        {
            _targetsProvider = targetsProvider;
            _filter = filter;
        }

        private readonly IAttackTargetsProvider _targetsProvider;
        private readonly ICollection<IDamageGetter> _filter;
        
        public ICollection<IDamageGetter> GetAttackTargets()
        {
            var list = new List<IDamageGetter>(_targetsProvider.GetAttackTargets());
            foreach (var filtered in _filter)
            {
                if (list.Contains(filtered)) list.Remove(filtered);
            }
            return list;
        }
    }
}