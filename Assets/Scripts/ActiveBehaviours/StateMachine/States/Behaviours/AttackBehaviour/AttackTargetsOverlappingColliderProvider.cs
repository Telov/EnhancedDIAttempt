using System.Collections.Generic;
using EnhancedDIAttempt.Damage;
using EnhancedDIAttempt.Utils.CollisionManager;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.States.InputBased
{
    public class AttackTargetsOverlappingColliderProvider : IAttackTargetsProvider
    {
        public AttackTargetsOverlappingColliderProvider(Collider2D collider)
        {
            _collider = collider;
        }

        private readonly Collider2D _collider;
        
        public ICollection<IDamageGetter> GetAttackTargets()
        {
            List<Collider2D> colliders = new List<Collider2D>();

            _collider.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);

            return colliders.GetAllTFromEachCollisionManager<IDamageGetter>();
        }
    }
}