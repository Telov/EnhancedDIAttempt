using System.Collections.Generic;
using EnhancedDIAttempt.ActiveBehaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.StateMachine;
using EnhancedDIAttempt.Utils.ZenjectAdditions;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class CommonActionsInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform actorTransform;
        [SerializeField] private Collider2D actorCollider;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float onGroundHorizontalSlowdownCoef;

        [Inject] private IUpdatesController _updatesController;

        public readonly DecorationProperty<ActorInfoProvider> ActorInfoProvider = new ();
        public readonly DecorationProperty<IGroundChecker> GroundChecker = new ();
        public readonly DecorationProperty<IBehavioursProvider> OnGroundStateBehaviours = new ();
        public readonly DecorationProperty<IState> OnGroundState = new ();
        public readonly DecorationProperty<IBehavioursProvider> InAirStateBehavioursProvider = new ();
        public readonly DecorationProperty<IState> InAirState = new ();
        public readonly DecorationProperty<IStatesProvider> StatesProvider = new ();
        public readonly DecorationProperty<IController> StateMachine = new ();

        public override void InstallBindings()
        {
            ActorInfoProvider.Set
            (
                new ActorInfoProvider
                (
                    actorTransform,
                    rb,
                    actorCollider
                )
            );

            GroundChecker.Set
            (
                new GroundChecker
                (
                    ActorInfoProvider.FinalValue,
                    whatIsGround,
                    ActorInfoProvider.FinalValue
                )
            );


            OnGroundStateBehaviours.Set
            (
                new SimpleBehavioursProvider
                (
                    new List<IBehaviour>()
                )
            );

            OnGroundState.Set
            (
                new OnGroundStateDecorator
                (
                    new StateBase(OnGroundStateBehaviours.FinalValue),
                    GroundChecker.FinalValue,
                    _updatesController,
                    rb,
                    onGroundHorizontalSlowdownCoef
                )
            );

            InAirStateBehavioursProvider.Set
            (
                new SimpleBehavioursProvider(new List<IBehaviour>())
            );

            InAirState.Set
            (
                new InAirStateDecorator
                (
                    new StateBase(InAirStateBehavioursProvider.FinalValue),
                    GroundChecker.FinalValue,
                    _updatesController
                )
            );

            StatesProvider.Set
            (
                new StatesProvider(
                    new List<IState>
                    {
                        OnGroundState.FinalValue,
                        InAirState.FinalValue
                    }
                )
            );

            StateMachine.Set
            (
                new ActiveBehavioursStateMachine(StatesProvider.FinalValue)
            );

            Container.Bind<IController>().FromInstance(StateMachine.FinalValue);
        }
    }
}