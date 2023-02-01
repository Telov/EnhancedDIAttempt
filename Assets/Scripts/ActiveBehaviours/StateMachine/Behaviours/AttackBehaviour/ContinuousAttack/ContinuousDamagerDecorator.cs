using System;
using System.Collections.Generic;
using EnhancedDIAttempt.Health;
using Telov.Utils;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class ContinuousDamagerDecorator : IContinuousDamager
    {
        public ContinuousDamagerDecorator
        (
            IDamager damager,
            IUpdatesController updatesController,
            IDamageablesProvider targetsProvider,
            IStopper stopper
        )
        {
            _damager = damager;
            _updatesController = updatesController;
            _targetsProvider = targetsProvider;
            _stopper = stopper;
        }

        private readonly IDamager _damager;
        private readonly IUpdatesController _updatesController;
        private readonly IDamageablesProvider _targetsProvider;
        private readonly IStopper _stopper;

        public event Action OnDamagingStarted = () => { };
        public event Action OnDamagingEnded = () => { };

        private Context _context;
        private float _amount;
        private List<object> _whoGotDamage;

        public void DealDamage(Context context, IEnumerable<IDamageable> damageGetters, float amount)
        {
            _context = context;
            _amount = amount;

            _stopper.OnStopped += StopAttack;
            _updatesController.UpdateCallbacks += Attack;

            _whoGotDamage = new List<object>();

            OnDamagingStarted();
        }

        private void StopAttack()
        {
            _stopper.OnStopped -= StopAttack;
            _updatesController.UpdateCallbacks -= Attack;
            OnDamagingEnded();
        }

        private void Attack()
        {
            var currentTargets = new List<IDamageable>(_targetsProvider.GetAttackTargets());

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

            _damager.DealDamage(_context, currentTargets, _amount);

            foreach (var target in currentTargets)
            {
                _whoGotDamage.Add(target);
            }
        }
    }
}