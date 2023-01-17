using EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours;
using Telov.Utils;
using UnityEngine;
using Zenject;

namespace EnhancedDIAttempt.Installers
{
    public class MobInstaller : MonoInstaller
    {
        [SerializeField] private float wanderingTimeInOneDirection;

        [Inject] private IUpdatesController _updatesController;
        [Inject] private CommonCharacterInstaller _commonCharacterInstaller;

        public override void DecorateProperties()
        {
            _commonCharacterInstaller.AttackBehaviour.AddPreSetRoutine
            (() =>
                {
                    _commonCharacterInstaller.AttackRuler.PrimaryValue = () =>
                        new AttackIfCanAttackRuler
                        (
                            _commonCharacterInstaller.AttackTargetsProvider.FinalValue,
                            _updatesController
                        );
                }
            );

            _commonCharacterInstaller.MoveRuler.PrimaryValue = () =>
                new WanderingMoveRuler
                (
                    _commonCharacterInstaller.coroutinesHost,
                    wanderingTimeInOneDirection
                );
        }
    }
}