using EnhancedDIAttempt.AnimationInteraction;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBasedActions;
using EnhancedDIAttempt.AnimationInteraction.MoveAction;
using EnhancedDIAttempt.Utils.MecanimStateMachine;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class PlayerActionsInstaller : MonoInstaller
    {
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
        [Inject] private CommonActionsInstaller _commonActionsInstaller;

        public override void InstallBindings()
        {
            var actionMap =
                new PlayerInputActions();
            actionMap.Enable();
            var inputActions = actionMap.PlayerActionMap;

            int animatorGroundedBoolId = Animator.StringToHash(animatorGroundedBoolName);

            var toggleableAttackAllower = new ToggleableAttackAllowerDecorator(new BaseAttackAllower(maxAttackDuration));

            var attackAnimatorStateInfoProvider = animator.GetBehaviour<AnimatorAttackStateExitedNotifier>();

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
                        attackAnimatorStateInfoProvider,
                        new AnimatorBoolSetter(animator, Animator.StringToHash(animatorAttackingBoolName))
                    ),
                    inputActions.Attack,
                    attackDamage
                );


            _commonActionsInstaller.OnGroundStateBehaviours.Decorate
            (
                x =>
                {
                    MoveActionData moveActionData =
                        new MoveActionData
                        (
                            _updatesController,
                            new RotatingCharacterRb2DMoverDecorator
                            (
                                new ForceMultipOnRunRb2DMoverDecorator
                                (
                                    new Rb2DMover(_commonActionsInstaller.ActorInfoProvider.FinalValue.GetRb()),
                                    inputActions.Run,
                                    runSpeedMultiplier
                                ),
                                _commonActionsInstaller.ActorInfoProvider.FinalValue.GetTransform()
                            )
                            ,
                            new AnimationDependantMoveAllowerDecorator
                            (
                                new SimpleMoveAllower(),
                                attackAnimatorStateInfoProvider
                            ),
                            inputActions.Movement
                        );

                    IBehaviour moveAction =
                        new MoveAction(movementSettings, moveActionData);

                    IBehaviour jumpAction =
                        new JumpAction
                        (
                            jumpSettings,
                            _commonActionsInstaller.ActorInfoProvider.FinalValue,
                            _commonActionsInstaller.ActorInfoProvider.FinalValue,
                            inputActions.Jump
                        );

                    return new BehavioursProviderDecorator
                    (
                        x,
                        moveAction,
                        jumpAction,
                        attackAction
                    );
                }
            );

            _commonActionsInstaller.OnGroundState.Decorate
            (
                x =>
                    new AnimatorBoolChangerStateDecorator
                    (
                        x,
                        new AnimatorBoolSetter(animator, animatorGroundedBoolId),
                        false
                    )
            );

            _commonActionsInstaller.InAirState.Decorate
            (
                x =>
                    new AnimatorBoolChangerStateDecorator
                    (
                        x,
                        new AnimatorBoolSetter(animator, animatorGroundedBoolId),
                        true
                    )
            );
        }
    }
}