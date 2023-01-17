using UnityEngine;

namespace EnhancedDIAttempt.Health
{
    public class ChangingGOLayerDeathHandlerDecorator : IDeathHandler
    {
        public ChangingGOLayerDeathHandlerDecorator(IDeathHandler deathHandler, GameObject gameObject, int layerNumber)
        {
            _deathHandler = deathHandler;
            _gameObject = gameObject;
            _layerNumber = layerNumber;
        }

        private readonly IDeathHandler _deathHandler;
        private readonly GameObject _gameObject;
        private readonly int _layerNumber;

        public void Trigger()
        {
            _gameObject.layer = _layerNumber;
            _deathHandler.Trigger();
        }
    }
}