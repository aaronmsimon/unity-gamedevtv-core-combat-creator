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
            if (PlayerInAttackRange() && fighter.CanAttack(player)) {
                fighter.Attack(player);
            } else {
                fighter.Cancel();
            }
        }

        private bool PlayerInAttackRange() {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
