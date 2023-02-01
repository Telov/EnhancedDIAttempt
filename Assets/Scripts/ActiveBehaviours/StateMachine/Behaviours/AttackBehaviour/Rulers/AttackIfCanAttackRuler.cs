using System;
using Telov.Utils;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class AttackIfCanAttackRuler : IAttackRuler
    {
        public AttackIfCanAttackRuler(
            IDamageablesProvider damageablesProvider,
            IUpdatesController updatesController)
        {
            _damageablesProvider = damageablesProvider;
            _updatesController = updatesController;
        }
        
        private readonly IDamageablesProvider _damageablesProvider;
        private readonly IUpdatesController _updatesController;
        
        public event Action OnWantAttack
        {
            add
            {
                _innerOnWantAttack += value;
                _listenersCount++;
                if (_listenersCount == 1) _updatesController.UpdateCallbacks += OnUpdate;
            }
            remove
            {
                _innerOnWantAttack -= value;
                _listenersCount--;
                if (_listenersCount == 0) _updatesController.UpdateCallbacks -= OnUpdate;
            }
        }

        private Action _innerOnWantAttack = () => { };

        private int _listenersCount;

        private void OnUpdate()
        {
            if (_damageablesProvider.GetAttackTargets().Count != 0) _innerOnWantAttack();
        }
    }
}