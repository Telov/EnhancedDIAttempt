using EnhancedDIAttempt.AnimationInteraction;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased;
using EnhancedDIAttempt.AnimationInteraction.MoveAction;
using EnhancedDIAttempt.Utils.MecanimStateMachine;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class PlayerActionsInstaller : MonoInstaller
    {
        [SerializeField] private InputBasedMoveRulerDecorator.MovementSettings movementSettings;
        [SerializeField] private float runSpeedMultiplier;
        [SerializeField] private JumpAction.JumpSettings jumpSettings;

        [Header("AttackAction")] [SerializeField]
        private Collider2D attackCollider;

        [SerializeField] private float attackCooldown;
        [SerializeField] private float maxAttackDuration;
        [SerializeField] private float attackDamage;
        [SerializeField] private string animatorAttackingBoolName;

        [Inject] private IUpdatesController _updatesController;
        [Inject] private CommonActionsInstaller _commonActionsInstaller;
        [Inject] private HealthInstaller _healthInstaller;

        public override void DecorateProperties()
        {
            var actionMap =
                new PlayerInputActions();
            actionMap.Enable();
            var inputActions = actionMap.PlayerActionMap;


            var toggleableAttackAllower = new ToggleableAttackAllowerDecorator(new BaseAttackAllower(maxAttackDuration));

            Animator animator = _commonActionsInstaller.animator;
            var attackAnimatorStateInfoProvider = animator.GetBehaviour<AnimatorAttackStateExitedNotifier>();


            _commonActionsInstaller.Rb2DMover.Decorate
            (x =>
                    new ForceMultipOnRunRb2DMoverDecorator
                    (
                        x,
                        inputActions.Run,
                        runSpeedMultiplier
                    )
            );

            _commonActionsInstaller.MoveRuler.Decorate
            ((x) =>
                new InputBasedMoveRulerDecorator
                (
                    x,
                    _updatesController,
                    new AnimationDependantMoveAllowerDecorator
                    (
                        new SimpleMoveAllower(),
                        attackAnimatorStateInfoProvider
                    ),
                    inputActions.Movement,
                    movementSettings
                )
            );

            _commonActionsInstaller.OnGroundStateBehaviours.Decorate
            (
                x =>
                {
                    IBehaviour jumpAction =
                        new JumpAction
                        (
                            jumpSettings,
                            _commonActionsInstaller.ActorInfoProvider.FinalValue,
                            _commonActionsInstaller.ActorInfoProvider.FinalValue,
                            inputActions.Jump
                        );

                    IBehaviour attackAction =
                        new AttackAction
                        (
                            new FilteringAttackTargetsProviderDecorator
                            (
                                new AttackTargetsOverlappingColliderProvider(attackCollider),
                                new[] { _healthInstaller.Health.FinalValue }
                            ),
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

                    return new BehavioursProviderCompositeDecorator
                    (
                        x,
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