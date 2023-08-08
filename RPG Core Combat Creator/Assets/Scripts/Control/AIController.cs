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
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointDwellTime = 2.5f;

        private Vector3 guardPos;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private int waypointIndex = 0;
        private float timeSinceReachedWaypoint = Mathf.Infinity;

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
                PatrolBehavior();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceReachedWaypoint += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPos = guardPos;

            if (patrolPath != null) {
                if (AtWaypoint()) {
                    timeSinceReachedWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPos = GetCurrentWaypoint();
            }

            if (timeSinceReachedWaypoint > waypointDwellTime) {
                mover.StartMoveToAction(nextPos);
            }
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

        private bool AtWaypoint() {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint <= waypointTolerance;
        }

        private void CycleWaypoint() {
            waypointIndex = patrolPath.GetNextIndex(waypointIndex);
        }

        private Vector3 GetCurrentWaypoint() {
            return patrolPath.GetWaypoint(waypointIndex);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
