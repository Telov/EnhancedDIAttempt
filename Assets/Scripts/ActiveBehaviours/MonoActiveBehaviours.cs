using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.ActiveBehaviours
{
    public class MonoActiveBehaviours : MonoBehaviour
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