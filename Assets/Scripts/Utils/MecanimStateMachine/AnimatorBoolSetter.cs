using UnityEngine;

namespace Telov.Utils
{
    public class AnimatorBoolSetter : IAnimatorBoolSetter
    {
        public AnimatorBoolSetter(Animator animator, int boolId)
        {
            _animator = animator;
            _boolId = boolId;
        }

        private readonly Animator _animator;
        private readonly int _boolId;

        public void SetBoolToTrue()
        {
            _animator.SetBool(_boolId, true);
        }

        public void SetBoolToFalse()
        {
            _animator.SetBool(_boolId, false);
        }
    }
}