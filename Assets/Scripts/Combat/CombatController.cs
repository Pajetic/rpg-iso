using RPGIso.Core;
using UnityEngine;
using RPGIso.Movement;

namespace RPGIso.Combat {
    public class CombatController : MonoBehaviour, IAction {
        // TODO use actual weapon data
        [SerializeField] private float weaponRange = 2f;

        private CombatTarget target;
        private MovementController movementController;
        private Animator animator;

        private void Start() {
            movementController = GetComponent<MovementController>();
            animator = GetComponent<Animator>();
        }

        private void Update() {
            if (target == null) {
                return;
            }
            
            if (!IsInRange()) {
                movementController.MoveTo(target.transform.position);
            }
            else {
                movementController.CancelAction();
                AttackAction();
            }
        }

        private void AttackAction() {
            animator.SetTrigger("attack");
        }

        public void Attack(CombatTarget target) {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target;
        }

        public void CancelAction() {
            target = null;
        }

        private bool IsInRange() {
            return Vector3.Distance(target.transform.position, transform.position) <= weaponRange;
        }

        // Animation event
        private void Hit() {
            
        }
    }
}
