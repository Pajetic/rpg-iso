using RPGIso.Core;
using UnityEngine;
using RPGIso.Movement;

namespace RPGIso.Combat {
    public class CombatController : MonoBehaviour, IAction {
        // TODO use actual weapon data
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float attackInterval = 2f;
        [SerializeField] private float damage = 10f;

        private GameObject target;
        private MovementController movementController;
        private Animator animator;
        private float timeSinceLastAttack = 0;

        private void Start() {
            movementController = GetComponent<MovementController>();
            animator = GetComponent<Animator>();
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null || !target.GetComponent<Health>().isAlive) {
                return;
            }
            
            if (!IsInRange()) {
                movementController.MoveTo(target.transform.position);
            } else {
                movementController.CancelAction();
                AttackAction();
            }
        }

        private void AttackAction() {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > attackInterval) {
                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        public bool CanAttack(GameObject target) {
            if (target == null || target.GetComponent<Health>() == null) {
                return false;
            }
            return target.GetComponent<Health>().isAlive;
        }

        public void Attack(GameObject target) {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target;
        }

        public void CancelAction() {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
            target = null;
        }

        private bool IsInRange() {
            return Vector3.Distance(target.transform.position, transform.position) <= weaponRange;
        }

        // Animation event
        private void Hit() {
            if (target == null) {
                return;
            }
            target.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
