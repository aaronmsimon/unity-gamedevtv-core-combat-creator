using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    private PlayerControls playerControls;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private bool buttonPressed = false;

    private void Awake() {
        playerControls = new PlayerControls();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        playerControls.Enable();
        playerControls.Player.Move.started += _ => buttonPressed = true;
        playerControls.Player.Move.canceled += _ => buttonPressed = false;
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Update() {
        Move();
        UpdateAnimator();
    }

    private void Move() {
        if (buttonPressed) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                navMeshAgent.destination = hitInfo.point;
            }
        }
    }

    private void UpdateAnimator() {
        Vector3 worldVelocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);
        float speed = localVelocity.z;

        animator.SetFloat("forwardSpeed", speed);
    }
}
