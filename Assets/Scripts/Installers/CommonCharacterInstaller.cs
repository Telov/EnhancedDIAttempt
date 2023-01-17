using System.Collections.Generic;
using EnhancedDIAttempt.ActiveBehaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours.ConstantMoveDownBehaviour;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.AnimationInteraction;
using EnhancedDIAttempt.Health;
using EnhancedDIAttempt.StateMachine;
using EnhancedDIAttempt.Utils.ZenjectAdditions;
using Telov.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class CommonCharacterInstaller : MonoInstaller
    {
        [SerializeField] public Animator animator;
        [SerializeField] public CollisionManager collisionManager;
        [SerializeField] public CoroutinesHost coroutinesHost;

        [FormerlySerializedAs("animEventsListener")] [SerializeField]
        public CommonCharacterAnimationsEventsNotifier animEventsNotifier;

        [SerializeField, Header("ActorInfo")] private Rigidbody2D rb;
        [SerializeField] private Transform actorTransform;
        [SerializeField] private Collider2D actorCollider;

        [SerializeField, Header("AttackBehaviour")]
        private float attackCooldown;

        [SerializeField] private float attackPower;

        [SerializeField] private Collider2D attackCollider;
        [SerializeField] private string animatorAttackingBoolName;

        [SerializeField, Header("WalkBehaviour")]
        private string animatorRunningBoolName;

        [SerializeField] private float moveSpeed;

        [SerializeField] private float postRunAnimationDuration;

        [SerializeField, Header("Ground related states")]
        private LayerMask whatIsGround;

        [SerializeField] public string animatorGroundedBoolName;
        [SerializeField] private float onGroundHorizontalSlowdownCoef;
        [SerializeField] private float moveDownPower;
        [SerializeField] private float downPowerRiseRate;
        [SerializeField] private float moveDownSpeedLimit;

        [Inject] private IUpdatesController _updatesController;

        public readonly DecorationProperty<ActorInfoProvider> ActorInfoProvider = new();
        public readonly DecorationProperty<IDeathHandler> DeathHandler = new();
        public readonly DecorationProperty<IRb2DMover> Rb2DMover = new();
        public readonly DecorationProperty<IMoveRuler> MoveRuler = new();
        public readonly DecorationProperty<IBlocker> WalkBehaviourBlocker = new();
        public readonly DecorationProperty<IBehaviour> WalkBehaviour = new();
        public readonly DecorationProperty<IAttackRuler> AttackRuler = new();
        public readonly DecorationProperty<IAttackTargetsProvider> AttackTargetsProvider = new();
        public readonly DecorationProperty<IBlocker> AttackBehaviourBlocker = new();
        public readonly DecorationProperty<IBehaviour> AttackBehaviour = new();
        public readonly DecorationProperty<IGroundChecker> GroundChecker = new();
        public readonly DecorationProperty<IBehavioursProvider> OnGroundStateBehaviours = new();
        public readonly DecorationProperty<IState> OnGroundState = new();
        public readonly DecorationProperty<IBehavioursProvider> InAirStateBehavioursProvider = new();
        public readonly DecorationProperty<IState> InAirState = new();
        public readonly DecorationProperty<IStatesProvider> StatesProvider = new();
        public readonly DecorationProperty<IController> StateMachine = new();

        [SerializeField, Header("Health")] private SpriteRenderer spriteRenderer;
        [SerializeField] private float startHealth;
        [SerializeField] private float maxHealth;
        [SerializeField] private float fadingOnDeathDuration;
        [SerializeField] private string animatorDeadBoolName;
        [SerializeField] private string animatorHurtTriggerName;
        [SerializeField] private int deadLayerNumber;

        public DecorationProperty<IHealthController> Health = new();

        public override void InstallBindings()
        {
            ActorInfoProvider.PrimaryValue = () =>
                new ActorInfoProvider
                (
                    actorTransform,
                    rb,
                    actorCollider
                );

            AttackBehaviourBlocker.PrimaryValue = () => new SimpleBlocker();
            WalkBehaviourBlocker.PrimaryValue = () => new SimpleBlocker();

            DeathHandler.PrimaryValue = () =>
                new ChangingGOLayerDeathHandlerDecorator
                (
                    new BlockingDeathHandler
                    (
                        new SettingAnimatorBoolDeathHandlerDecorator
                        (
                            new FadingSpriteDeathHandlerDecorator
                            (
                                new GODestroyerDeathHandler(ActorInfoProvider.FinalValue.GetTransform().gameObject),
                                coroutinesHost,
                                spriteRenderer,
                                fadingOnDeathDuration
                            ),
                            new AnimatorBoolSetter(animator, Animator.StringToHash(animatorDeadBoolName))
                        ),
                        WalkBehaviourBlocker.FinalValue,
                        AttackBehaviourBlocker.FinalValue
                    ),
                    ActorInfoProvider.FinalValue.GetCollider().gameObject,
                    deadLayerNumber
                );

            Health.PrimaryValue = () =>
                new HealthCollisionSubscriberDecorator
                (
                    new TriggeringAnimationHealthDecorator
                    (
                        new DyingHealthDecorator
                        (
                            new HealthLoggerDecorator
                            (
                                new SimpleHealth(startHealth, maxHealth)
                            ),
                            DeathHandler.FinalValue
                        ),
                        new AnimatorTriggerSetter(animator, animatorHurtTriggerName)
                    ),
                    collisionManager
                );

            Container.Bind<IHealthController>().FromInstance(Health.FinalValue).AsSingle();

            GroundChecker.PrimaryValue = () =>
                new GroundChecker
                (
                    ActorInfoProvider.FinalValue,
                    whatIsGround,
                    ActorInfoProvider.FinalValue
                );

            var attackInterrupter = new BaseAttackInterrupter();

            AttackTargetsProvider.PrimaryValue = () =>
                new FilteringAttackTargetsProviderDecorator
                (
                    new OverlappingColliderAttackTargetsProvider(attackCollider),
                    new[] { Health.FinalValue }
                );

            AttackBehaviour.PrimaryValue = () =>
                    new BlockableBehaviourDecorator
                    (
                        new AttackBehaviour
                        (
                            AttackRuler.FinalValue,
                            new CoolingDownDamageDealerDecorator
                            (
                                new DependantOnAnimationContinuousDamageDealerDecorator
                                (
                                    new ContinuousDamageDealerDecorator
                                    (
                                        new SimpleDamageDealer(),
                                        coroutinesHost,
                                        AttackTargetsProvider.FinalValue,
                                        attackInterrupter
                                    ),
                                    attackInterrupter,
                                    animEventsNotifier,
                                    new AnimatorBoolSetter(animator, Animator.StringToHash(animatorAttackingBoolName))
                                ),
                                new SimpleReloader(attackCooldown)
                            ),
                            AttackTargetsProvider.FinalValue,
                            attackPower
                        ),
                        AttackBehaviourBlocker.FinalValue
                    )
                ;

            Rb2DMover.PrimaryValue = () =>
                new RotatingCharacterRb2DMoverDecorator
                (
                    new SettingAnimatorBoolRb2DMoverDecorator
                    (
                        new Rb2DMover(ActorInfoProvider.FinalValue.GetRb()),
                        coroutinesHost,
                        new AnimatorBoolSetter(animator, Animator.StringToHash(animatorRunningBoolName)),
                        postRunAnimationDuration
                    ),
                    ActorInfoProvider.FinalValue.GetTransform()
                );

            WalkBehaviour.PrimaryValue = () =>
                new BlockableBehaviourDecorator
                (
                    new MoveBehaviour(Rb2DMover.FinalValue, MoveRuler.FinalValue, moveSpeed),
                    WalkBehaviourBlocker.FinalValue
                );

            OnGroundStateBehaviours.PrimaryValue = () =>
                new SimpleBehavioursProvider
                (
                    new List<IBehaviour> { AttackBehaviour.FinalValue, WalkBehaviour.FinalValue }
                );


            int animatorGroundedBoolId = Animator.StringToHash(animatorGroundedBoolName);

            OnGroundState.PrimaryValue = () =>
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
                );

            var moveDownBehaviour =
                new ConstantMoveDownBehaviour
                (
                    ActorInfoProvider.FinalValue.GetRb(),
                    _updatesController,
                    moveDownPower,
                    downPowerRiseRate,
                    moveDownSpeedLimit
                );

            InAirStateBehavioursProvider.PrimaryValue = () =>
                new SimpleBehavioursProvider(new List<IBehaviour> { moveDownBehaviour });

            InAirState.PrimaryValue = () =>
                new AnimatorBoolChangerStateDecorator
                (
                    new InAirStateDecorator
                    (
                        new StateBase(InAirStateBehavioursProvider.FinalValue),
                        GroundChecker.FinalValue,
                        _updatesController
                    ),
                    new AnimatorBoolSetter(animator, animatorGroundedBoolId),
                    true
                );

            StatesProvider.PrimaryValue = () =>
                new StatesProvider(
                    new List<IState>
                    {
                        OnGroundState.FinalValue,
                        InAirState.FinalValue
                    }
                );

            StateMachine.PrimaryValue = () => new ActiveBehavioursStateMachine(StatesProvider.FinalValue);

            Container.Bind<IController>().FromInstance(StateMachine.FinalValue).AsSingle();
        }
    }
}