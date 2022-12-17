using System.Collections.Generic;
using EnhancedDIAttempt.AnimationInteraction;
using EnhancedDIAttempt.PlayerActions;
using EnhancedDIAttempt.PlayerActions.StateMachine;
using EnhancedDIAttempt.PlayerActions.StateMachine.States;
using EnhancedDIAttempt.PlayerActions.StateMachine.States.Actions;
using EnhancedDIAttempt.StateMachine;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class PlayerMovementInstaller : MonoInstaller
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private MoveAction.MovementSettings movementSettings;
        [SerializeField] private JumpAction.JumpSettings jumpSettings;
        [SerializeField] private Animator animator;
        [SerializeField] private string animatorGroundedBoolName;
        [Header("AttackAction")]
        [SerializeField] private Collider2D attackCollider;
        [SerializeField] private ContinuousAttackAction.ContinuousAttackInfo continuousAttackInfo;
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

            MoveActionData moveActionData =
                new MoveActionData
                (
                    playerInfoProvider,
                    groundChecker,
                    _updatesController,
                    inputActions.Movement
                );
            
            IBehaviour moveAction =
                new MoveAction(movementSettings, moveActionData);
            
            IBehaviour jumpAction =
                new JumpAction(jumpSettings, playerInfoProvider, playerInfoProvider, inputActions.Jump);

            IBehaviour attackAction =
                new ContinuousAttackActionWithAnimation(
                    inputActions.Attack,
                    new AttackTargetsOverlappingColliderProvider(attackCollider),
                    continuousAttackInfo,
                    animator.GetBehaviour<AttackStateMachineBehaviour>(),
                    animator,
                    Animator.StringToHash(animatorAttackingBoolName));
            
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
                        _updatesController
                    ),
                    animator,
                    animatorGroundedBoolId,
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
                    animator,
                    animatorGroundedBoolId,
                    true
                );

            #endregion

            ISpareStatesProvider spareStatesProvider =
                new MovementConfigurator(onGroundState, inAirState);

            MovementStateMachine stateMachine =
                new MovementStateMachine(spareStatesProvider);

            Container.Bind<IController>().FromInstance(stateMachine);
        }
    }
}