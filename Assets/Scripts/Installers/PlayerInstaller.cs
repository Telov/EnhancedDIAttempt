using EnhancedDIAttempt.ActiveBehaviours.StateMachine;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using EnhancedDIAttempt.ActiveBehaviours.StateMachine.States;
using EnhancedDIAttempt.Health;
using EnhancedDIAttempt.Input;
using Telov.Utils;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputController inputController;
        [SerializeField] private float runSpeedMultiplier;
        [SerializeField] private float jumpPower;
        [SerializeField] private float jumpCooldown;

        [Inject] private IUpdatesController _updatesController;
        [Inject] private CommonCharacterInstaller _commonCharacterInstaller;

        public override void DecorateProperties()
        {
            var actionMap =
                inputController.GetInputClassInstance();
            var inputActions = actionMap.PlayerActionMap;

            _commonCharacterInstaller.DeathHandler.Decorate
            (x =>
                new DisablingInputControllerDeathHandlerDecorator
                (
                    x,
                    inputController
                )
            );

            _commonCharacterInstaller.AttackBehaviour.AddPreSetRoutine
            (() =>
                {
                    _commonCharacterInstaller.AttackRuler.PrimaryValue = () =>
                        new AttackOnInputAttackRuler
                        (
                            inputActions.Attack
                        );
                }
            );

            _commonCharacterInstaller.WalkBehaviour.AddPreSetRoutine
            (() =>
                {
                    _commonCharacterInstaller.Rb2DMover.Decorate
                    (x =>
                        new ForceMultipOnRunRb2DMoverDecorator
                        (
                            x,
                            inputActions.Run,
                            runSpeedMultiplier
                        )
                    );
                    _commonCharacterInstaller.MoveRuler.PrimaryValue = () =>
                        new InputBasedMoveRuler
                        (
                            _updatesController,
                            new SimpleMoveAllower(),
                            inputActions.Movement
                        );
                }
            );

            _commonCharacterInstaller.OnGroundStateBehaviours.Decorate
            (
                x =>
                {
                    IBehaviour jumpAction =
                        new JumpBehaviour
                        (
                            jumpPower,
                            new SimpleReloader(jumpCooldown),
                            new SimpleJumper
                            (
                                _commonCharacterInstaller.ActorInfoProvider.FinalValue.GetTransform(),
                                _commonCharacterInstaller.ActorInfoProvider.FinalValue.GetRb()
                            ),
                            new InputBasedJumpRuler(inputActions.Jump)
                        );

                    return new CompositeBehaviour(x, jumpAction);
                }
            );
        }
    }
}