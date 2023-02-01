using System.Collections.Generic;
using EnhancedDIAttempt.ActiveBehaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours.ConstantMoveDownBehaviour;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.AnimationInteraction;
using EnhancedDIAttempt.AnimationInteraction.SMBehaviours;
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
        //[SerializeField] public CoroutinesHost coroutinesHost;

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
        [SerializeField] private float maxTimeBetweenClickAndAttackStart;

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
        public readonly DecorationProperty<IBlockable> WalkBehaviourBlock = new();
        public readonly DecorationProperty<IBehaviour> WalkBehaviour = new();
        public readonly DecorationProperty<IAttackRuler> AttackRuler = new();
        public readonly DecorationProperty<IDamageablesProvider> AttackTargetsProvider = new();
        public readonly DecorationProperty<IBlockable> AttackBehaviourBlock = new();
        public readonly DecorationProperty<IBehaviour> AttackBehaviour = new();
        public readonly DecorationProperty<IBehaviour> OnGroundStateBehaviours = new();
        public readonly DecorationProperty<IState> OnGroundState = new();
        public readonly DecorationProperty<IBehaviour> InAirBehaviours = new();
        public readonly DecorationProperty<IState> InAirState = new();

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

            AttackBehaviourBlock.PrimaryValue = () => new SimpleBlockable();
            WalkBehaviourBlock.PrimaryValue = () => new SimpleBlockable();

            IBlocker walkBlocker = new SimpleBlocker(WalkBehaviourBlock.FinalValue);

            IBlocker allGroundBehsBlocker =
                new SimpleBlocker
                (
                    new CompositeBlockable
                    (
                        WalkBehaviourBlock.FinalValue,
                        AttackBehaviourBlock.FinalValue
                    )
                );

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
                                _updatesController,
                                spriteRenderer,
                                fadingOnDeathDuration
                            ),
                            new AnimatorBoolSetter(animator, Animator.StringToHash(animatorDeadBoolName))
                        ),
                        allGroundBehsBlocker
                    ),
                    ActorInfoProvider.FinalValue.GetCollider().gameObject,
                    deadLayerNumber
                );

            Health.PrimaryValue = () =>
                new HealthCollisionSubscriberDecorator
                (
                    new BlockingHealthDecorator
                    (
                        new SettingAnimatorBoolHealthDecorator
                        (
                            new DyingHealthDecorator
                            (
                                new HealthLoggerDecorator
                                (
                                    new SimpleHealth(startHealth, maxHealth)
                                ),
                                DeathHandler.FinalValue
                            ),
                            new AnimatorBoolSetter(animator, Animator.StringToHash(animatorHurtTriggerName)),
                            animEventsNotifier
                        ),
                        animEventsNotifier,
                        allGroundBehsBlocker
                    ),
                    collisionManager
                );

            Container.Bind<IHealthController>().FromInstance(Health.FinalValue).AsSingle();

            IGroundChecker groundChecker =
                new GroundChecker
                (
                    ActorInfoProvider.FinalValue,
                    whatIsGround,
                    ActorInfoProvider.FinalValue
                );

            var attackStopper = new SimpleStopper();

            AttackTargetsProvider.PrimaryValue = () =>
                new FilteringDamageablesProviderDecorator
                (
                    new OverlappingColliderDamageablesProvider(attackCollider),
                    new[] { Health.FinalValue }
                );
            
            AttackBehaviour.PrimaryValue = () =>
                new BlockableBehaviourDecorator
                (
                    new AttackBehaviour
                    (
                        AttackRuler.FinalValue,
                        new CoolingDownDamagerDecorator
                        (
                            new DependantOnAnimationContinuousDamagerDecorator
                            (
                                new BlockingContinuousDamagerDecorator
                                (
                                    new ContinuousDamagerDecorator
                                    (
                                        new SimpleDamager(),
                                        _updatesController,
                                        AttackTargetsProvider.FinalValue,
                                        attackStopper
                                    ),
                                    walkBlocker
                                ),
                                new AnimatorBoolSetter(animator, Animator.StringToHash(animatorAttackingBoolName)),
                                animEventsNotifier,
                                animator.GetBehaviour<AttackMecanimState>(),
                                maxTimeBetweenClickAndAttackStart,
                                attackStopper
                            ),
                            new SimpleReloader(attackCooldown)
                        ),
                        AttackTargetsProvider.FinalValue,
                        attackPower
                    ),
                    AttackBehaviourBlock.FinalValue
                );

            Rb2DMover.PrimaryValue = () =>
                new RotatingCharacterRb2DMoverDecorator
                (
                    new SettingAnimatorBoolRb2DMoverDecorator
                    (
                        new Rb2DMover(ActorInfoProvider.FinalValue.GetRb()),
                        _updatesController,
                        new AnimatorBoolSetter(animator, Animator.StringToHash(animatorRunningBoolName)),
                        postRunAnimationDuration
                    ),
                    ActorInfoProvider.FinalValue.GetTransform()
                );

            WalkBehaviour.PrimaryValue = () =>
                new BlockableBehaviourDecorator
                (
                    new MoveBehaviour(Rb2DMover.FinalValue, MoveRuler.FinalValue, moveSpeed),
                    WalkBehaviourBlock.FinalValue
                );

            IAnimatorBoolSetter groundedBoolSetter = new AnimatorBoolSetter(animator, Animator.StringToHash(animatorGroundedBoolName));

            OnGroundStateBehaviours.PrimaryValue = () => new CompositeBehaviour(AttackBehaviour.FinalValue, WalkBehaviour.FinalValue);

            OnGroundState.PrimaryValue = () =>
                new AnimatorBoolChangerStateDecorator
                (
                    new OnGroundStateDecorator
                    (
                        new StateBase(OnGroundStateBehaviours.FinalValue),
                        groundChecker,
                        _updatesController,
                        rb,
                        onGroundHorizontalSlowdownCoef
                    ),
                    groundedBoolSetter,
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

            InAirBehaviours.PrimaryValue = () => new CompositeBehaviour(moveDownBehaviour);

            InAirState.PrimaryValue = () =>
                new AnimatorBoolChangerStateDecorator
                (
                    new InAirStateDecorator
                    (
                        new StateBase(InAirBehaviours.FinalValue),
                        groundChecker,
                        _updatesController
                    ),
                    groundedBoolSetter,
                    true
                );

            IStatesProvider statesProvider = new StatesProvider(OnGroundState.FinalValue, InAirState.FinalValue);

            IController controller = new ActiveBehavioursStateMachine(statesProvider);

            Container.Bind<IController>().FromInstance(controller).AsSingle();
        }
    }
}