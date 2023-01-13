using EnhancedDIAttempt.Damage;
using EnhancedDIAttempt.Health;
using EnhancedDIAttempt.Utils.CollisionManager;
using EnhancedDIAttempt.Utils.ZenjectAdditions;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class HealthInstaller : MonoInstaller
    {
        [SerializeField] private CollisionManager collisionManager;
        [SerializeField] private float startHealth;
        [SerializeField] private float maxHealth;

        public DecorationProperty<IHealthController> Health = new();

        public override void InstallBindings()
        {
            Health.Set
            (
                new HealthCollisionSubscriberDecorator
                (
                    new HealthLoggerDecorator
                    (
                        new SimpleHealth(startHealth, maxHealth)
                    ),
                    collisionManager
                )
            );
            Container.Bind<IHealthController>().FromInstance(Health.FinalValue);
        }
    }
}