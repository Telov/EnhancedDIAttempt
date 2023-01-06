using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.MobActions;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class MobActionsInstaller : MonoInstaller
    {
        [SerializeField] private float wanderingTimeInOneDirection;
        [SerializeField] private float wanderSpeed;

        [Inject] private IUpdatesController _updatesController;
        [Inject] private CommonActionsInstaller _commonActionsInstaller;

        public override void InstallBindings()
        {
            _commonActionsInstaller.OnGroundStateBehaviours.Decorate
            (x =>
                new BehavioursProviderDecorator
                (
                    x,
                    new WanderingAction
                    (
                        _updatesController,
                        new Rb2DMover(_commonActionsInstaller.ActorInfoProvider.FinalValue.GetRb()),
                        wanderingTimeInOneDirection,
                        wanderSpeed
                    )
                )
            );
        }
    }
}