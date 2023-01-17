using System;
using System.Collections;
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
            ICoroutinesHost coroutinesHost,
            IAttackTargetsProvider targetsProvider,
            IAttackInterrupter interrupter
        )
        {
            _damageDealer = damageDealer;
            _coroutinesHost = coroutinesHost;
            _targetsProvider = targetsProvider;
            _interrupter = interrupter;
        }

        private readonly IDamageDealer _damageDealer;
        private readonly ICoroutinesHost _coroutinesHost;
        private readonly IAttackTargetsProvider _targetsProvider;
        private readonly IAttackInterrupter _interrupter;

        public event Action OnAttackStarted = () => { };
        public event Action OnAttackEnded = () => { };

        private Context _context;
        private Coroutine _coroutine;

        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            _coroutine = _coroutinesHost.StartCoroutine(AttackCoroutine(amount));
            _context = context;
            _context.OnDeactivated += StopAttack;
            _interrupter.OnWantToStopAttack += StopAttack;
        }

        private IEnumerator AttackCoroutine(float amount)
        {
            var whoGotDamage = new List<object>();

            OnAttackStarted();

            while (true)
            {
                var currentTargets = new List<IDamageGetter>(_targetsProvider.GetAttackTargets());

                //remove targets which already got damage
                for (int i = 0; i < currentTargets.Count; i++)
                {
                    var target = currentTargets[i];

                    if (whoGotDamage.Contains(target))
                    {
                        currentTargets.Remove(target);
                        i--;
                    }
                }

                _damageDealer.DealDamage(_context, currentTargets, amount);

                foreach (var target in currentTargets)
                {
                    whoGotDamage.Add(target);
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private void StopAttack()
        {
            _context.OnDeactivated -= StopAttack;
            _interrupter.OnWantToStopAttack -= StopAttack;
            _coroutinesHost.StopCoroutine(_coroutine);
            OnAttackEnded();
        }
    }
}