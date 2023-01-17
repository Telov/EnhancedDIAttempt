using System.Collections.Generic;
using EnhancedDIAttempt.Damage;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class CoolingDownDamageDealerDecorator : IDamageDealer
    {
        public CoolingDownDamageDealerDecorator(IDamageDealer damageDealer, IReloader reloader)
        {
            _damageDealer = damageDealer;
            _reloader = reloader;
        }

        private readonly IDamageDealer _damageDealer;
        private readonly IReloader _reloader;
        
        public void DealDamage(Context context, IEnumerable<IDamageGetter> damageGetters, float amount)
        {
            if(!_reloader.Reloaded) return;
            
            _reloader.Unload();
            _damageDealer.DealDamage(context, damageGetters, amount);
        }
    }
}