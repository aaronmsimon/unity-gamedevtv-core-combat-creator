using UnityEngine;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Vector3 guardPos;

        private GameObject player;
        private Fighter fighter;
        private Mover mover;

        private void Awake() {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
        }

        private void Start() {
            guardPos = transform.position;
        }

        private void Update() {
            if (PlayerInAttackRange() && fighter.CanAttack(player)) {
                fighter.Attack(player);
            } else {
                mover.StartMoveToAction(guardPos); // this will cancel the fight action, too!
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
