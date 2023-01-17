namespace EnhancedDIAttempt.ActiveBehaviours.StateMachine.Behaviours
{
    public class AttackBehaviour : IBehaviour
    {
        public AttackBehaviour
        (
            IAttackRuler ruler,
            IDamageDealer damageDealer,
            IAttackTargetsProvider targetsProvider,
            float power
        )
        {
            _ruler = ruler;
            _damageDealer = damageDealer;
            _targetsProvider = targetsProvider;
            _power = power;
        }

        private readonly IAttackRuler _ruler;
        private readonly IDamageDealer _damageDealer;
        private readonly IAttackTargetsProvider _targetsProvider;
        private readonly float _power;

        private readonly Context _context = new Context();

        public void Activate(EnhancedDIAttempt.StateMachine.StateMachine.CallbackContext callbackContext)
        {
            _ruler.OnWantAttack += Attack;
        }

        public void Deactivate()
        {
            _ruler.OnWantAttack -= Attack;
            _context.InvokeDeactivated();
        }

        private void Attack()
        {
            _damageDealer.DealDamage(_context,_targetsProvider.GetAttackTargets(), _power);
        }
    }
}