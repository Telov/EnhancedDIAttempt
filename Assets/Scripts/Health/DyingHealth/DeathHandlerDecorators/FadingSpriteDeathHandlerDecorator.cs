using System.Collections;
using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.Health
{
    public class FadingSpriteDeathHandlerDecorator : IDeathHandler
    {
        public FadingSpriteDeathHandlerDecorator(IDeathHandler deathHandler, ICoroutinesHost host, SpriteRenderer renderer, float duration)
        {
            _deathHandler = deathHandler;
            _host = host;
            _renderer = renderer;
            _duration = duration;
        }

        private readonly IDeathHandler _deathHandler;
        private readonly ICoroutinesHost _host;
        private readonly SpriteRenderer _renderer;
        private readonly float _duration;

        public void Trigger()
        {
            _host.StartCoroutine(FadeCoroutine());
        }

        private IEnumerator FadeCoroutine()
        {
            float timeLeft = _duration;
            
            while (timeLeft > 0f)
            {
                yield return new WaitForEndOfFrame();
                timeLeft -= Time.deltaTime;
                
                var color = _renderer.color;
                color.a = timeLeft / _duration;
                _renderer.color = color;
            }
            
            _deathHandler.Trigger();
        }
    }
}