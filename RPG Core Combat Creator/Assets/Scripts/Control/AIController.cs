using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float susTime = 2f;

        private Vector3 guardPos;
        private float timeSinceLastSawPlayer = Mathf.Infinity;

        private GameObject player;
        private Fighter fighter;
        private Mover mover;
        private Health health;

        private void Awake() {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }

        private void Start() {
            guardPos = transform.position;
        }

        private void Update() {
            if (health.IsDead()) return;

            if (PlayerInAttackRange() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < susTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior(); // this will cancel the fight action, too!
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehavior()
        {
            mover.StartMoveToAction(guardPos);
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
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
