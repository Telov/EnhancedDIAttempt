using EnhancedDIAttempt.CharacterRotator;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class VelocityBasedCharacterRotatorInstaller : MonoInstaller
    {
        [SerializeField] private Vector3 rotationAxis;
        [SerializeField] private Transform rotatedObject;
        [SerializeField] private new Rigidbody2D rigidbody;
        [Inject] private IUpdatesController _updatesController;

        public override void InstallBindings()
        {
            Container.Bind<ICharacterRotator>()
                .FromInstance(
                    new VelocityBasedCharacterRotator(
                        _updatesController,
                        rotatedObject,
                        rigidbody,
                        rotationAxis)
                );
        }
    }
}