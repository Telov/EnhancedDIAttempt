using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.Utils.MecanimStateMachine;
using UnityEngine;

namespace EnhancedDIAttempt.AnimationInteraction.MoveAction
{
    public class SettingAnimatorBoolRb2DMoverDecorator : IRb2DMover
    {
        public SettingAnimatorBoolRb2DMoverDecorator(IRb2DMover mover, IAnimatorBoolSetter boolSetter, float animationLengthAfterLastMoveInSec)
        {
            _mover = mover;
            _boolSetter = boolSetter;
            _animationLengthAfterLastMoveInSec = animationLengthAfterLastMoveInSec;
        }

        private readonly IRb2DMover _mover;
        private readonly IAnimatorBoolSetter _boolSetter;
        private readonly float _animationLengthAfterLastMoveInSec;

        private readonly object _lockObject = new();
        private float _timeLeft;
        
        public void Move(float speedMultiplier, Vector3 direction)
        {
            TryStartCountDown();
            _boolSetter.SetBoolToTrue();
            _mover.Move(speedMultiplier, direction);
        }
        
        private async void TryStartCountDown()
        {
            _timeLeft = _animationLengthAfterLastMoveInSec;
            bool alreadyTaken = false;
            Monitor.Enter(_lockObject, ref alreadyTaken);
            if (alreadyTaken) return;
            
            while (_timeLeft > 0f)
            {
                _timeLeft -= Time.deltaTime;
                await UniTask.NextFrame();
            }
            
            _boolSetter.SetBoolToFalse();
        }
    }
}