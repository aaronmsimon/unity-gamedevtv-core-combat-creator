using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 5f;

        private Health target;
        private float timeSinceLastAttack;

        private Mover mover;
        private Animator animator;

        private void Awake() {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Start() {
            timeSinceLastAttack = 0;
        }

        private void Update() {
            if (target == null) return;
            if (target.IsDead()) return;

            if (!DetermineIsInRange()) {
                mover.MoveTo(target.transform.position);
            } else {
                mover.Cancel();
                AttackBehavior();
            }

            timeSinceLastAttack += Time.deltaTime;
        }

        private bool DetermineIsInRange() {
            return Vector3.Distance(target.transform.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }

        private void AttackBehavior() {
            if (timeSinceLastAttack >= timeBetweenAttacks) {
                transform.LookAt(target.transform);

                animator.ResetTrigger("cancelAttack");
                // This will trigger the Hit event
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        public bool CanAttack(CombatTarget combatTarget) {
            if (combatTarget == null) return false;
            return combatTarget != null && !combatTarget.GetComponent<Health>().IsDead();
        }

        public void Cancel() {
            target = null;
            animator.ResetTrigger("attack");
            animator.SetTrigger("cancelAttack");
        }

        // Animation Event
        private void Hit() {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }
    }
}
