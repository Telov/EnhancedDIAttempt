using UnityEngine;

namespace Telov.Utils
{
    public class AnimatorTriggerSetter : IAnimatorTriggerSetter
    {
        public AnimatorTriggerSetter(Animator animator, int triggerHash)
        {
            _animator = animator;
            _triggerHash = triggerHash;
        }

        private readonly Animator _animator;
        private readonly int _triggerHash;
        
        public void SetTrigger()
        {
            _animator.SetTrigger(_triggerHash);
        }
    }
}