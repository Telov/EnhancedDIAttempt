using System.Collections.Generic;
using EnhancedDIAttempt.Health;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class CoolingDownDamagerDecorator : IDamager
    {
        public CoolingDownDamagerDecorator(IDamager damager, IReloader reloader)
        {
            _damager = damager;
            _reloader = reloader;
        }

        private readonly IDamager _damager;
        private readonly IReloader _reloader;
        
        public void DealDamage(Context context, IEnumerable<IDamageable> damageGetters, float amount)
        {
            if(!_reloader.Reloaded) return;
            
            _reloader.Unload();
            _damager.DealDamage(context, damageGetters, amount);
        }
    }
}