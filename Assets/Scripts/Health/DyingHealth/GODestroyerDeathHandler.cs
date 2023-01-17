using UnityEngine;

namespace EnhancedDIAttempt.Health
{
    public class GODestroyerDeathHandler : IDeathHandler
    {
        public GODestroyerDeathHandler(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        private readonly GameObject _gameObject;

        public void Trigger()
        {
            UnityEngine.Object.Destroy(_gameObject);
        }
    }
}