using UnityEngine;
using RPGIso.Movement;
using RPGIso.Combat;

namespace RPGIso.Control {
    class PlayerController : MonoBehaviour {

        MovementController movementController;
        CombatController combatController;
        private Ray lastRay;

        private void Start() {
            movementController = GetComponent<MovementController>();
            combatController = GetComponent<CombatController>();
        }

        private void Update() {
            if (HandleCombat()) {
                return;
            }

            if (HandleMovement()) {
                return;
            }
            Debug.Log("Nothing to do.");
        }

        private bool HandleMovement() {
            
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit)) {
                if (Input.GetMouseButton(0)) {
                    movementController.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private bool HandleCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) {
                GameObject target = hit.transform.gameObject;
                if (target.GetComponent<CombatTarget>() != null && combatController.CanAttack(target)) {
                    if (Input.GetMouseButtonDown(0)) {
                        combatController.Attack(target);
                    }
                    return true;
                }
            }

            return false;
        }

        private Ray GetMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}