using UnityEngine;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Fighter fighter;

        private GameObject player;

        private void Awake() {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update() {
            if (DistanceToPlayer() <= chaseDistance) {
                PerformCombat();
            } else {
                fighter.Cancel();
            }
        }

        private float DistanceToPlayer() {
            return Vector3.Distance(transform.position, player.transform.position);
        }

        private void PerformCombat()
        {
            GameObject target = player;
            
            if (target == null) return;

            if (!fighter.CanAttack(target.gameObject)) return;

            fighter.Attack(target.gameObject);
        }
    }
}
