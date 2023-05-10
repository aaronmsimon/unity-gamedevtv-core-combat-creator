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
            if (target != null) {
                mover.MoveTo(target.position);
                if (Mathf.Abs((target.position - transform.position).magnitude) < weaponRange) {
                    mover.Stop();
                }
            }
        }

        public void Attack(CombatTarget combatTarget) {
            target = combatTarget.transform;
        }
    }
}
