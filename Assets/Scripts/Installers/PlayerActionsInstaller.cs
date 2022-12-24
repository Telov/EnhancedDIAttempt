using System.Collections.Generic;
using EnhancedDIAttempt.AnimationInteraction;
using EnhancedDIAttempt.ActiveBehaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.Actions;
using EnhancedDIAttempt.AnimationInteraction.MoveAction;
using EnhancedDIAttempt.StateMachine;
using EnhancedDIAttempt.Utils.MecanimStateMachine;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class PlayerActionsInstaller : MonoInstaller
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float onGroundHorizontalSlowdownCoef;
        [SerializeField] private MoveAction.MovementSettings movementSettings;
        [SerializeField] private float runSpeedMultiplier;
        [SerializeField] private JumpAction.JumpSettings jumpSettings;
        [SerializeField] private Animator animator;
        [SerializeField] private string animatorGroundedBoolName;

        [Header("AttackAction")] [SerializeField]
        private Collider2D attackCollider;

        [SerializeField] private float attackCooldown;
        [SerializeField] private float maxAttackDuration;
        [SerializeField] private float attackDamage;
        [SerializeField] private string animatorAttackingBoolName;

        [Inject] private IUpdatesController _updatesController;

        public override void InstallBindings()
        {
            var actionMap =
                new PlayerInputActions();
            actionMap.Enable();
            var inputActions = actionMap.PlayerActionMap;

            PlayerInfoProvider playerInfoProvider =
                new PlayerInfoProvider
                (
                    playerTransform,
                    rb,
                    playerCollider
                );

            IGroundChecker groundChecker =
                new GroundChecker
                (
                    playerInfoProvider,
                    whatIsGround,
                    playerInfoProvider
                );

            int animatorGroundedBoolId = Animator.StringToHash(animatorGroundedBoolName);

            #region OnGroundState

            var toggleableAttackAllower = new ToggleableAttackAllowerDecorator(new BaseAttackAllower(maxAttackDuration));

            var AttackAnimatorStateInfoProvider = animator.GetBehaviour<AnimatorAttackStateExitedNotifier>();

            IBehaviour attackAction =
                new AttackAction
                (
                    new AttackTargetsOverlappingColliderProvider(attackCollider),
                    new SimpleCooldownController(attackCooldown),
                    new DependantOnAnimationContinuousDamageDealerDecorator
                    (
                        new ContinuousDamageDealerDecorator
                        (
                            new SimpleDamageDealer(),
                            toggleableAttackAllower
                        ),
                        toggleableAttackAllower,
                        AttackAnimatorStateInfoProvider,
                        new AnimatorBoolSetter(animator, Animator.StringToHash(animatorAttackingBoolName))
                    ),
                    inputActions.Attack,
                    attackDamage
                );

            MoveActionData moveActionData =
                new MoveActionData
                (
                    _updatesController,
                    new ForceMultipOnRunRb2DMoverDecorator
                    (
                        new Rb2DMover(rb),
                        inputActions.Run,
                        runSpeedMultiplier
                    ),
                    new AnimationDependantMoveAllowerDecorator
                    (
                        new SimpleMoveAllower(),
                        AttackAnimatorStateInfoProvider
                    ),
                    inputActions.Movement
                );

            IBehaviour moveAction =
                new MoveAction(movementSettings, moveActionData);

            IBehaviour jumpAction =
                new JumpAction(jumpSettings, playerInfoProvider, playerInfoProvider, inputActions.Jump);


            List<IBehaviour> onGroundStateBehaviours =
                new List<IBehaviour>
                {
                    moveAction,
                    jumpAction,
                    attackAction
                };

            IState onGroundState =
                new AnimatorBoolChangerStateDecorator
                (
                    new OnGroundStateDecorator
                    (
                        new StateBase(onGroundStateBehaviours),
                        groundChecker,
                        _updatesController,
                        rb,
                        onGroundHorizontalSlowdownCoef
                    ),
                    new AnimatorBoolSetter(animator, animatorGroundedBoolId),
                    false
                );

            #endregion

            #region InAirState

            List<IBehaviour> inAirStateBehaviours = new List<IBehaviour>();

            IState inAirState =
                new AnimatorBoolChangerStateDecorator
                (
                    new InAirStateDecorator
                    (
                        new StateBase(inAirStateBehaviours),
                        groundChecker,
                        _updatesController
                    ),
                    new AnimatorBoolSetter(animator, animatorGroundedBoolId),
                    true
                );

            #endregion

            ISpareStatesProvider spareStatesProvider =
                new ActiveBehaviours.StateMachine.ActiveBehaviours(onGroundState, inAirState);

            ActiveBehavioursStateMachine stateMachine =
                new ActiveBehavioursStateMachine(spareStatesProvider);

            Container.Bind<IController>().FromInstance(stateMachine);
        }
    }
}