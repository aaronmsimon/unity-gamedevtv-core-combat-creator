using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private Fighter fighter;

        private void Awake() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
        }

        private void Update() {
            UpdateAnimator();
        }

        public void StartMoveToAction(Vector3 destination) {
            fighter.Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = destination;
            GetComponent<ActionScheduler>().StartAction(this);
        }

        public void Stop() {
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
