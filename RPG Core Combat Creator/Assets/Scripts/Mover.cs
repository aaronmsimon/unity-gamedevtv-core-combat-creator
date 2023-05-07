using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    private PlayerControls playerControls;
    private NavMeshAgent navMeshAgent;

    private void Awake() {
        playerControls = new PlayerControls();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable() {
        playerControls.Enable();
        playerControls.Player.Move.performed += Move;
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Move(InputAction.CallbackContext ctx) {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) {
            navMeshAgent.destination = hitInfo.point;
        }
    }
}
