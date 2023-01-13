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
        [SerializeField] private string animatorRunningBoolName;
        [SerializeField] private JumpAction.JumpSettings jumpSettings;

        [Header("AttackAction")] [SerializeField]
        private Collider2D attackCollider;

        [SerializeField] private float attackCooldown;
        [SerializeField] private float maxAttackDuration;
        [SerializeField] private float attackDamage;
        [SerializeField] private string animatorAttackingBoolName;

        [Inject] private IUpdatesController _updatesController;
        [Inject] private CommonActionsInstaller _commonActionsInstaller;

        public override void DecorateProperties()
        {
            var actionMap =
                new PlayerInputActions();
            actionMap.Enable();
            var inputActions = actionMap.PlayerActionMap;


            var toggleableAttackAllower = new ToggleableAttackAllowerDecorator(new BaseAttackAllower(maxAttackDuration));

            Animator animator = _commonActionsInstaller.animator;
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
                            new ForceMultipOnRunRb2DMoverDecorator
                            (
                                _commonActionsInstaller.Rb2DMover.FinalValue,
                                inputActions.Run,
                                runSpeedMultiplier
                            ),
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

                    return new BehavioursProviderCompositeDecorator
                    (
                        x,
                        moveAction,
                        jumpAction,
                        attackAction
                    );
                }
            );

            _commonActionsInstaller.InAirState.Decorate
            (
                x =>
                    new AnimatorBoolChangerStateDecorator
                    (
                        x,
                        new AnimatorBoolSetter(animator, Animator.StringToHash(_commonActionsInstaller.animatorGroundedBoolName)),
                        true
                    )
            );
        }
    }
}