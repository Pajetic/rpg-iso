using UnityEngine;
using RPGIso.Movement;
using RPGIso.Combat;

namespace RPGIso.Control {
    class PlayerController : MonoBehaviour {

        MovementController movementController;
        private Ray lastRay;

        private void Start() {
            movementController = GetComponent<MovementController>();
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
                    movementController.MoveTo(hit.point);
                }

                return true;
            }
            return false;
        }

        private bool HandleCombat() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target != null) {
                    if (Input.GetMouseButtonDown(0)) {
                        GetComponent<CombatController>().Attack(target);
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