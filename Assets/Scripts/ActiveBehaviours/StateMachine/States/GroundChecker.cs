using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States
{
    public class GroundChecker : IGroundChecker
    {
        public GroundChecker(
            IActorHeightProvider actorHeightProvider,
            LayerMask whatIsGround,
            IActorCenterProvider actorCenterProvider)
        {
            _actorHeightProvider = actorHeightProvider;
            _whatIsGround = whatIsGround;
            _actorCenterProvider = actorCenterProvider;
        }

        private readonly IActorHeightProvider _actorHeightProvider;
        private readonly LayerMask _whatIsGround;
        private readonly IActorCenterProvider _actorCenterProvider;

        public bool Grounded =>
            Physics2D.Raycast(
                _actorCenterProvider.GetCenter(),
                Vector3.down,
                _actorHeightProvider.GetHeight() * 0.5f + 0.05f,
                _whatIsGround);
    }
}