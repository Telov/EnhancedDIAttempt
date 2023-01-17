using System;
using System.Collections;
using Telov.Utils;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class WanderingMoveRuler : IMoveRuler
    {
        public WanderingMoveRuler(ICoroutinesHost host, float wanderTimeInOneDirection)
        {
            _host = host;
            _wanderTimeInOneDirection = wanderTimeInOneDirection;
        }

        private readonly ICoroutinesHost _host;
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
                if (_listenersCount == 1) _host.StartCoroutine(Wander());
            }
            remove
            {
                _innerOnWantMove -= value;
                _listenersCount--;
                if (_listenersCount == 0) _host.StopCoroutine(Wander());
            }
        }

        private Action<Vector3> _innerOnWantMove = (_) => { };

        private IEnumerator Wander()
        {
            while (true)
            {
                _innerOnWantMove(_goingToTheRight ? Vector2.right : Vector2.left);

                yield return new WaitForFixedUpdate();
                
                _timeWalkedInOneDirection += Time.fixedDeltaTime;
                if (_timeWalkedInOneDirection > _wanderTimeInOneDirection)
                {
                    _goingToTheRight = !_goingToTheRight;
                    _timeWalkedInOneDirection = 0f;
                }
            }
        }
    }
}