using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.Health
{
    public class FadingSpriteDeathHandlerDecorator : IDeathHandler
    {
        public FadingSpriteDeathHandlerDecorator(IDeathHandler deathHandler, IUpdatesController updatesController, SpriteRenderer renderer, float duration)
        {
            _deathHandler = deathHandler;
            _updatesController = updatesController;
            _renderer = renderer;
            _duration = duration;
        }

        private readonly IDeathHandler _deathHandler;
        private readonly IUpdatesController _updatesController;
        private readonly SpriteRenderer _renderer;
        private readonly float _duration;

        private float _timeLeft;

        public void Trigger()
        {
            _timeLeft = _duration;
            _updatesController.AddUpdateCallback(FadeCoroutine);
        }

        private void FadeCoroutine()
        {
            _timeLeft -= Time.deltaTime;

            var color = _renderer.color;
            color.a = _timeLeft / _duration;
            _renderer.color = color;

            if (!(_timeLeft > 0f))
            {
                _deathHandler.Trigger();
                _updatesController.RemoveUpdateCallback(FadeCoroutine);
            }
        }
    }
}