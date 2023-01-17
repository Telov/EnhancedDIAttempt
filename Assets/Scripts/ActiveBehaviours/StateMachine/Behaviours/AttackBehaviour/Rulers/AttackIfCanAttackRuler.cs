using System;
using Telov.Utils;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class AttackIfCanAttackRuler : IAttackRuler
    {
        public AttackIfCanAttackRuler(
            IAttackTargetsProvider attackTargetsProvider,
            IUpdatesController updatesController)
        {
            _attackTargetsProvider = attackTargetsProvider;
            _updatesController = updatesController;
        }
        
        private readonly IAttackTargetsProvider _attackTargetsProvider;
        private readonly IUpdatesController _updatesController;
        
        public event Action OnWantAttack
        {
            add
            {
                _innerOnWantAttack += value;
                _listenersCount++;
                if (_listenersCount == 1) _updatesController.AddUpdateCallback(OnUpdate);
            }
            remove
            {
                _innerOnWantAttack -= value;
                _listenersCount--;
                if (_listenersCount == 0) _updatesController.RemoveUpdateCallback(OnUpdate);
            }
        }

        private Action _innerOnWantAttack = () => { };

        private int _listenersCount;

        private void OnUpdate()
        {
            if (_attackTargetsProvider.GetAttackTargets().Count != 0) _innerOnWantAttack();
        }
    }
}