using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.PlayerActions
{
    public class PlayerActions : MonoBehaviour
    {
        [Inject]
        private void Construct(IController controller)
        {
            _controller = controller;
        }

        private IController _controller;

        private void OnEnable()
        {
            _controller.Enable();
        }

        private void OnDisable()
        {
            _controller.Disable();
        }
    }
}