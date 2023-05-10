using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerControls playerControls;
        private bool buttonPressed = false;

        private Mover mover;
        private Fighter fighter;

        private void Awake() {
            playerControls = new PlayerControls();

            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
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
            if (InteractWithCombat()) return;
            if (InteractWithMove()) return;
            Debug.Log("Nothing to do.");
        }

        private bool InteractWithMove() {
            RaycastHit hitInfo;

            if (Physics.Raycast(GetMouseRay(), out hitInfo))
            {
                if (buttonPressed) {
                    mover.StartMoveToAction(hitInfo.point);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (playerControls.Player.Move.triggered) {
                    fighter.Attack(target);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        }
    }
}
