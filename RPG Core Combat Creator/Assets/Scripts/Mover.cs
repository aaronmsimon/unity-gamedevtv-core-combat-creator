using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Ray lastRay;
    private PlayerControls playerControls;

    private NavMeshAgent navMeshAgent;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
        playerControls.Player.Move.performed += Move;
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Move(InputAction.CallbackContext ctx) {
        lastRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.red, 3f);
        // navMeshAgent.destination = target.position;
    }
}
