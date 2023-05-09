using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    private bool buttonPressed = false;
    private Mover mover;

    private void Awake() {
        playerControls = new PlayerControls();
        mover = GetComponent<Mover>();
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
    }

    private void Move() {
        if (buttonPressed) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo)) {
                mover.MoveTo(hitInfo.point);
            }
        }
    }
}
