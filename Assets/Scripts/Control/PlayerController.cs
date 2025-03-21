using UnityEngine;
using RPGIso.Movement;

namespace RPGIso.Control {
    class PlayerController : MonoBehaviour {

        MovementController movementController;
        private Ray lastRay;

        private void Start() {
            movementController = GetComponent<MovementController>();
        }

        private void Update() {
            if (Input.GetMouseButton(0)) {
                MoveToCursor();
            }
        }

        private void MoveToCursor() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)) {
                movementController.MoveTo(hit.point);
            }
        }

    }
}