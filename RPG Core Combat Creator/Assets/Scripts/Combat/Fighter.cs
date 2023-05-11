using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float damageAmount = 5f;

        private Transform target;
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

            if (!DetermineIsInRange()) {
                mover.MoveTo(target.position);
            } else {
                mover.Cancel();
                AttackBehavior();
            }

            timeSinceLastAttack += Time.deltaTime;
        }

        private bool DetermineIsInRange() {
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);
        }

        private void AttackBehavior() {
            if (timeSinceLastAttack >= timeBetweenAttacks) {
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
                target.GetComponent<Health>().TakeDamage(damageAmount);
            }
        }

        public void Cancel() {
            target = null;
        }

        // Animation Event
        private void Hit() {
            
        }
    }
}
