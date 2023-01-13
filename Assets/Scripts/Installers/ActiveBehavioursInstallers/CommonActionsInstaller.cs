using System.Collections.Generic;
using EnhancedDIAttempt.ActiveBehaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.AnimationInteraction;
using EnhancedDIAttempt.AnimationInteraction.MoveAction;
using EnhancedDIAttempt.StateMachine;
using EnhancedDIAttempt.Utils.MecanimStateMachine;
using EnhancedDIAttempt.Utils.ZenjectAdditions;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class CommonActionsInstaller : MonoInstaller
    {
        [SerializeField] public Animator animator;
        [SerializeField] public string animatorGroundedBoolName;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform actorTransform;
        [SerializeField] private Collider2D actorCollider;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float onGroundHorizontalSlowdownCoef;
        [SerializeField] public string animatorRunningBoolName;

        [Inject] private IUpdatesController _updatesController;

        public readonly DecorationProperty<ActorInfoProvider> ActorInfoProvider = new();
        public readonly DecorationProperty<IGroundChecker> GroundChecker = new();
        public readonly DecorationProperty<IBehavioursProvider> OnGroundStateBehaviours = new();
        public readonly DecorationProperty<IState> OnGroundState = new();
        public readonly DecorationProperty<IBehavioursProvider> InAirStateBehavioursProvider = new();
        public readonly DecorationProperty<IState> InAirState = new();
        public readonly DecorationProperty<IStatesProvider> StatesProvider = new();
        public readonly DecorationProperty<IController> StateMachine = new();
        public readonly DecorationProperty<IRb2DMover> Rb2DMover = new();

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


            int animatorGroundedBoolId = Animator.StringToHash(animatorGroundedBoolName);

            Rb2DMover.Set
            (
                new RotatingCharacterRb2DMoverDecorator
                (
                    new SettingAnimatorBoolRb2DMoverDecorator
                    (
                        new Rb2DMover(ActorInfoProvider.FinalValue.GetRb()),
                        new AnimatorBoolSetter(animator, Animator.StringToHash(animatorRunningBoolName)),
                        0.5f
                    ),
                    ActorInfoProvider.FinalValue.GetTransform()
                )
            );
            OnGroundState.Set
            (
                new AnimatorBoolChangerStateDecorator
                (
                    new OnGroundStateDecorator
                    (
                        new StateBase(OnGroundStateBehaviours.FinalValue),
                        GroundChecker.FinalValue,
                        _updatesController,
                        rb,
                        onGroundHorizontalSlowdownCoef
                    ),
                    new AnimatorBoolSetter(animator, animatorGroundedBoolId),
                    false
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