using EnhancedDIAttempt.Health;
using EnhancedDIAttempt.Utils.CollisionManager;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class HealthInstaller : MonoInstaller
    {
        [SerializeField] private CollisionManager collisionManager;
        [SerializeField] private float startHealth;
        [SerializeField] private float maxHealth;

        public override void InstallBindings()
        {
            IHealthController health =
                new HealthCollisionSubscriberDecorator
                (
                    new HealthLoggerDecorator
                    (
                        new SimpleHealth(startHealth, maxHealth)
                    ),
                    collisionManager
                );
            Container.Bind<IHealthController>().FromInstance(health);
        }
    }
}