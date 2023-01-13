using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public class JumpAction : IBehaviour
    {
        public JumpAction
        (
            JumpSettings jumpSettings,
            IActorRbProvider actorRbProvider,
            IActorTransformProvider actorTransformProvider,
            InputAction jumpAction
        )
        {
            _jumpSettings = jumpSettings;
            _rb = actorRbProvider.GetRb();
            _playerTransform = actorTransformProvider.GetTransform();
            _jumpAction = jumpAction;
        }
    
        private readonly JumpSettings _jumpSettings;
        private readonly Transform _playerTransform;//connection
        private readonly Rigidbody2D _rb;
    
        private readonly InputAction _jumpAction;
        private bool _readyToJump = true;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _jumpAction.started += OnJumpPressed;
        }

        public void Deactivate()
        {
            _jumpAction.started -= OnJumpPressed;
        }
        
        private async void OnJumpPressed(InputAction.CallbackContext ctx)
        {
            
            if (_readyToJump)
            {
                Jump();
    
                await UniTask.Delay(TimeSpan.FromSeconds(_jumpSettings.JumpCooldown));
                
                ResetJump();
            }
    
            void Jump()
            {
                _readyToJump = false;
                
                // reset y velocity
                var velocity = _rb.velocity;
                _rb.velocity = new Vector2(velocity.x, 0f);
    
                _rb.AddForce(_playerTransform.up * _jumpSettings.JumpForce, ForceMode2D.Impulse);
            }
    
            void ResetJump()
            {
                _readyToJump = true;
            }
        }
        
        [System.Serializable]
        public class JumpSettings
        {
            public float JumpCooldown;
            public float JumpForce;
        }
    }
}
