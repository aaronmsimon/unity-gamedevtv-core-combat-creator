using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange;

        private Transform target;

        private Mover mover;
        private Animator animator;

        private void Awake() {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update() {
            if (target == null) return;

            if (!DetermineIsInRange()) {
                mover.MoveTo(target.position);
            } else {
                mover.Cancel();
                AttackBehavior();
            }
        }

        private bool DetermineIsInRange() {
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);
        }

        private void AttackBehavior() {
            animator.SetTrigger("attack");
        }

        public void Cancel() {
            target = null;
        }

        // Animation Event
        private void Hit() {
            
        }
    }
}
