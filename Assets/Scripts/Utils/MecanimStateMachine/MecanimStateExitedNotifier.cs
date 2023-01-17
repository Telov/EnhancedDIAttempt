using System;
using UnityEngine;

namespace Telov.Utils
{
    public class MecanimStateExitedNotifier : StateMachineBehaviour, IMecanimStateInfoProvider
    {
        public event Action OnStateExited = () => { };
        public event Action OnStateEntered = () => { };

        private bool _active;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _active = true;
            OnStateEntered();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _active = false;
            OnStateExited();
        }

        public bool IsActive => _active;
    }
}