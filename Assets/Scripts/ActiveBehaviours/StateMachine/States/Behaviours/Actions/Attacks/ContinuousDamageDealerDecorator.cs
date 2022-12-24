using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EnhancedDIAttempt.Damage;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions
{
    public class ContinuousDamageDealerDecorator : IContinuousDamageDealer
    {
        public ContinuousDamageDealerDecorator
        (
            IDependantDamageDealer damageDealer,
            IAttackAllower attackAllower
        )
        {
            _damageDealer = damageDealer;
            _attackAllower = attackAllower;
        }

        private readonly IDependantDamageDealer _damageDealer;
        private readonly IAttackAllower _attackAllower;
        
        public event Action OnAttackStarted = () => { };
        public event Action OnAttackEnded = () => { };

        public async void DealDamage(IAttackTargetsProvider targetsProvider, float amount)
        {
            OnAttackStarted();
            var whoGotDamage = new List<object>();

            float timePassed = 0f;

            while (_attackAllower.AttackContinues(timePassed))
            {
                var currentTargets = new List<IDamageGetter>(targetsProvider.GetAttackTargets());

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

                foreach (var target in currentTargets)
                {
                    whoGotDamage.Add(target);
                }

                _damageDealer.DealDamage(currentTargets, amount);

                await UniTask.NextFrame();

                timePassed += Time.deltaTime;
            }

            OnAttackEnded();
        }
    }
}