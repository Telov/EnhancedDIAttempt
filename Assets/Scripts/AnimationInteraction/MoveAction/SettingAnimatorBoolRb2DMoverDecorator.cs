using System.Collections;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction
{
    public class SettingAnimatorBoolRb2DMoverDecorator : IRb2DMover
    {
        public SettingAnimatorBoolRb2DMoverDecorator
        (
            IRb2DMover mover,
            ICoroutinesHost coroutinesHost,
            IAnimatorBoolSetter boolSetter,
            float animationLengthAfterLastMoveInSec
        )
        {
            _mover = mover;
            _coroutinesHost = coroutinesHost;
            _boolSetter = boolSetter;
            _animationLengthAfterLastMoveInSec = animationLengthAfterLastMoveInSec;
        }

        private readonly IRb2DMover _mover;
        private readonly ICoroutinesHost _coroutinesHost;
        private readonly IAnimatorBoolSetter _boolSetter;
        private readonly float _animationLengthAfterLastMoveInSec;

        private bool _countdownStarted;
        private float _timeLeft;

        public void Move(float speedMultiplier, Vector3 direction)
        {
            _coroutinesHost.StartCoroutine(CountdownCoroutine());
            _boolSetter.SetBoolToTrue();
            _mover.Move(speedMultiplier, direction);
        }

        private IEnumerator CountdownCoroutine()
        {
            _timeLeft = _animationLengthAfterLastMoveInSec;
            if (_countdownStarted) yield break;
            _countdownStarted = true;
            
            while (_timeLeft > 0f)
            {
                _timeLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            
            _boolSetter.SetBoolToFalse();
            _countdownStarted = false;
        }
    }
}