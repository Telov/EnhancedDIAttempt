using System;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Interfaces;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.MobActions
{
    public class WanderingMoveRulerDecorator : IMoveRuler
    {
        public WanderingMoveRulerDecorator(IMoveRuler moveRuler, IUpdatesController updatesController, float wanderTimeInOneDirection, float wanderSpeed)
        {
            _moveRuler = moveRuler;
            _updatesController = updatesController;
            _wanderTimeInOneDirection = wanderTimeInOneDirection;
            _wanderSpeed = wanderSpeed;
        }

        private readonly IMoveRuler _moveRuler;
        private readonly IUpdatesController _updatesController;
        private readonly float _wanderTimeInOneDirection;
        private readonly float _wanderSpeed;

        private float _timeWalkedInOneDirection;
        private bool _goingToTheRight = true;

        private int _listenersCount;
        
        public event Action<float, Vector3> OnWantMove
        {
            add
            {
                _innerOnWantMove += value;
                _listenersCount++;
                _moveRuler.OnWantMove += value;
                if (_listenersCount == 1) _updatesController.AddFixedUpdateCallback(Wander);
            }
            remove
            {
                _innerOnWantMove -= value;
                _listenersCount--;
                _moveRuler.OnWantMove -= value;
                if (_listenersCount == 0) _updatesController.RemoveFixedUpdateCallback(Wander);
            }
        }

        private Action<float, Vector3> _innerOnWantMove = (_, _) => { };

        private void Wander()
        {
            _innerOnWantMove(_wanderSpeed, _goingToTheRight ? Vector2.right : Vector2.left);
            _timeWalkedInOneDirection += Time.fixedDeltaTime;
            if (_timeWalkedInOneDirection > _wanderTimeInOneDirection)
            {
                _goingToTheRight = !_goingToTheRight;
                _timeWalkedInOneDirection = 0f;
            }
        }
    }
}