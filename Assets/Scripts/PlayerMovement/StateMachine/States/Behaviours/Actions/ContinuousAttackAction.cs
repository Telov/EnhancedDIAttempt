using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EnhancedDIAttempt.Damage;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions
{
    public class ContinuousAttackAction : AttackActionBase
    {
        public ContinuousAttackAction(InputAction attackAction, IAttackTargetsProvider targetsProvider, ContinuousAttackInfo attackInfo)
            : base(targetsProvider, attackAction, attackInfo)
        {
        }
        
        private float _timePassed = 0f;
        private bool _working = false;

        protected override async void AttackInner(InputAction.CallbackContext callbackContext)
        {
            if (_working) return;
            _working = true;
                
            List<object> whoGotDamage = new List<object>();

            while (ContinueWorkingCondition())
            {
                var currentTargets = new List<IDamageGetter>(TargetsProvider.GetAttackTargets());

                //remove targets which already got damage
                for (int i = 0; i < currentTargets.Count; i++)
                {
                    IDamageGetter target = currentTargets[i];

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
                
                DealDamageToEach(currentTargets);

                await UniTask.NextFrame();

                _timePassed += Time.deltaTime;
            }
            
            SetCooldownPoint();
            _working = false;
        }

        protected virtual bool ContinueWorkingCondition()
        {
            return _timePassed < (Info as ContinuousAttackInfo).attackDuration;
        }

        [System.Serializable]
        public class ContinuousAttackInfo : AttackInfo
        {
            public float attackDuration;
        }
    }
}