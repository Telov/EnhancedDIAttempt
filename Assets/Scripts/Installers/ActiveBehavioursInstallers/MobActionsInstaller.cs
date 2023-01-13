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

        public override void DecorateProperties()
        {
            _commonActionsInstaller.MoveRuler.Decorate
            (x =>
                new WanderingMoveRulerDecorator
                (
                    x,
                    _updatesController,
                    wanderingTimeInOneDirection,
                    wanderSpeed
                )
            );
        }
    }
}