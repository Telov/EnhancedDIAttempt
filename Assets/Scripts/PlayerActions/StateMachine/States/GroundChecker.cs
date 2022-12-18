using UnityEngine;

namespace EnhancedDIAttempt.PlayerActions.StateMachine.States
{
    public class GroundChecker : IGroundChecker
    {
        public GroundChecker(
            IPlayerHeightProvider playerHeightProvider,
            LayerMask whatIsGround,
            IPlayerCenterProvider playerCenterProvider)
        {
            _playerHeightProvider = playerHeightProvider;
            _whatIsGround = whatIsGround;
            _playerCenterProvider = playerCenterProvider;
        }

        private readonly IPlayerHeightProvider _playerHeightProvider;
        private readonly LayerMask _whatIsGround;
        private readonly IPlayerCenterProvider _playerCenterProvider;

        public bool IsPlayerGrounded()
        {
            return Physics2D.Raycast(
                _playerCenterProvider.GetCenter(),
                Vector3.down,
                _playerHeightProvider.GetHeight() * 0.5f + 0.05f,
                _whatIsGround);
        }
    }
}