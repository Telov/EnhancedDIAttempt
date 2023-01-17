
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class JumpBehaviour : IBehaviour
    {
        public JumpBehaviour(float jumpPower, IReloader reloader, IJumper jumper, IJumpRuler jumpRuler)
        {
            _jumpPower = jumpPower;
            _reloader = reloader;
            _jumper = jumper;
            _jumpRuler = jumpRuler;
        }

        private readonly float _jumpPower;
        private readonly IReloader _reloader;
        private readonly IJumper _jumper;
        private readonly IJumpRuler _jumpRuler;

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _jumpRuler.OnWantToJump += Jump;
        }

        public void Deactivate()
        {
            _jumpRuler.OnWantToJump -= Jump;
        }

        private void Jump()
        {
            if (_reloader.Reloaded)
            {
                Debug.Log(123);
                _jumper.Jump(_jumpPower);
                _reloader.Unload();
            }
        }
    }
}