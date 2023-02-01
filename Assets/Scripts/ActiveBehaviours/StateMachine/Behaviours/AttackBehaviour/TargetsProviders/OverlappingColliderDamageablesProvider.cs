using System.Collections.Generic;
using EnhancedDIAttempt.Health;
using Telov.Utils;
using UnityEngine;

namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class OverlappingColliderDamageablesProvider : IDamageablesProvider
    {
        public OverlappingColliderDamageablesProvider(Collider2D collider)
        {
            _collider = collider;
        }

        private readonly Collider2D _collider;
        
        public ICollection<IDamageable> GetAttackTargets()
        {
            List<Collider2D> colliders = new List<Collider2D>();

            _collider.OverlapCollider(new ContactFilter2D().NoFilter(), colliders);

            return colliders.GetAllTFromEachCollisionManager<IDamageable>();
        }
    }
}