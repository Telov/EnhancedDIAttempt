using UnityEngine;

namespace Telov.Utils
{
    public class AnimatorTriggerSetter : IAnimatorTriggerSetter
    {
        public AnimatorTriggerSetter(Animator animator, string triggerName)
        {
            _animator = animator;
            _triggerHash = Animator.StringToHash(triggerName);
        }

        private readonly Animator _animator;
        private readonly int _triggerHash;
        
        public void SetTrigger()
        {
            _animator.SetTrigger(_triggerHash);
        }
    }
}