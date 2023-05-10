using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;

        private void Awake() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update() {
            UpdateAnimator();
        }

        public void StartMoveToAction(Vector3 destination) {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = destination;
        }

        public void Cancel() {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator() {
            Vector3 worldVelocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);
            float speed = localVelocity.z;

            animator.SetFloat("forwardSpeed", speed);
        }
    }
}
