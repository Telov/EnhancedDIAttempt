using System;
using System.Collections.Generic;
using EnhancedDIAttempt.Damage;
using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class ContinuousDamageDealerDecorator : IContinuousDamageDealer
    {
        public ContinuousDamageDealerDecorator
        (
            IDamageDealer damageDealer,
            IUpdatesController updatesController,
            IAttackTargetsProvider targetsProvider,
            IAttackInterrupter interrupter
        )
        {
            _damageDealer = damageDealer;
            _updatesController = updatesController;
            _targetsProvider = targetsProvider;
            _interrupter = interrupter;
        }

        private readonly IDamageDealer _damageDealer;
        private readonly IUpdatesController _updatesController;
        private readonly IAttackTargetsProvider _targetsProvider;
        private readonly IAttackInterrupter _interrupter;

        public event Action OnAttackStarted = () => { };
        public event Action OnAttackEnded = () => { };

        private Context _context;
        private float _amount;
        private List<object> _whoGotDamage;

        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            _context = context;
            _amount = amount;
            _updatesController.AddUpdateCallback(Attack);
            _context.OnDeactivated += InnerStopAttack;
            _interrupter.OnWantToStopAttack += InnerStopAttack;

            _whoGotDamage = new List<object>();

            OnAttackStarted();
        }

        private void Attack()
        {
            var currentTargets = new List<IDamageGetter>(_targetsProvider.GetAttackTargets());

            //remove targets which already got damage
            for (int i = 0; i < currentTargets.Count; i++)
            {
                var target = currentTargets[i];

                if (_whoGotDamage.Contains(target))
                {
                    currentTargets.Remove(target);
                    i--;
                }
            }

            _damageDealer.DealDamage(_context, currentTargets, _amount);

            foreach (var target in currentTargets)
            {
                _whoGotDamage.Add(target);
            }
        }

        private void InnerStopAttack()
        {
            _context.OnDeactivated -= InnerStopAttack;
            _interrupter.OnWantToStopAttack -= InnerStopAttack;
            _updatesController.RemoveUpdateCallback(Attack);
            OnAttackEnded();
        }
    }
}