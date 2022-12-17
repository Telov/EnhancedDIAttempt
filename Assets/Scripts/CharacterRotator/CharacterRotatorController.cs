using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.CharacterRotator
{
    public class CharacterRotatorController : MonoBehaviour
    {
        [Inject]
        private void Construct(ICharacterRotator rotator)
        {
            _rotator = rotator;
        }

        private ICharacterRotator _rotator;

        private void OnEnable()
        {
            _rotator.Activate();
        }

        private void OnDisable()
        {
            _rotator.Deactivate();
        }
    }
}