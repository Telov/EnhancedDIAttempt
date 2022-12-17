using System;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class AttackStateMachineBehaviour : StateMachineBehaviour, IAttackStateExitedNotifier
    {
        public event Action OnAttackStateExited = () => { };
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnAttackStateExited.Invoke();
        }
    }
}