using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Health
{
    public class Health : MonoBehaviour
    {
        [Inject]
        private void Construct(IHealthController controller)
        {
            _controller = controller;
        }

        private IHealthController _controller;

        private void OnEnable()
        {
            _controller.Activate();
        }

        private void OnDisable()
        {
            _controller.Deactivate();
        }
    }
}