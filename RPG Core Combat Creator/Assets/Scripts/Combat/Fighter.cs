using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange;

        private Transform target;

        private Mover mover;

        private void Awake() {
            mover = GetComponent<Mover>();
        }

        private void Update() {
            if (target != null && !DetermineIsInRange()) {
                mover.MoveTo(target.position);
            } else {
                mover.Stop();
            }
        }

        private bool DetermineIsInRange() {
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.transform;
        }

        public void Cancel() {
            target = null;
        }
    }
}
