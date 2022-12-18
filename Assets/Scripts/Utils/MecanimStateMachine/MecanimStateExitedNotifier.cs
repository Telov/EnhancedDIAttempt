using System;
using UnityEngine;

namespace EnhancedDIAttempt.Utils.MecanimStateMachine
{
    public class MecanimStateExitedNotifier : StateMachineBehaviour, IMecanimStateExitedNotifier
    {
        public event Action OnStateExited = () => { };
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnStateExited();
        }
    }
}