using System;
using Telov.Utils;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class WanderingMoveRuler : IMoveRuler
    {
        public WanderingMoveRuler(IUpdatesController updatesController, float wanderTimeInOneDirection)
        {
            _updatesController = updatesController;
            _wanderTimeInOneDirection = wanderTimeInOneDirection;
        }

        private readonly IUpdatesController _updatesController;
        private readonly float _wanderTimeInOneDirection;

        private float _timeWalkedInOneDirection;
        private bool _goingToTheRight = true;

        private int _listenersCount;

        public event Action<Vector3> OnWantMove
        {
            add
            {
                _innerOnWantMove += value;
                _listenersCount++;
                if (_listenersCount == 1) _updatesController.AddFixedUpdateCallback(Wander);
            }
            remove
            {
                _innerOnWantMove -= value;
                _listenersCount--;
                if (_listenersCount == 0) _updatesController.RemoveFixedUpdateCallback(Wander);
            }
        }

        private Action<Vector3> _innerOnWantMove = (_) => { };

        private void Wander()
        {
            _innerOnWantMove(_goingToTheRight ? Vector2.right : Vector2.left);

            _timeWalkedInOneDirection += Time.fixedDeltaTime;
            if (_timeWalkedInOneDirection > _wanderTimeInOneDirection)
            {
                _goingToTheRight = !_goingToTheRight;
                _timeWalkedInOneDirection = 0f;
            }
        }
    }
}