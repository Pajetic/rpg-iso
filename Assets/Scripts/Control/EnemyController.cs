using RPGIso.Combat;
using RPGIso.Core;
using RPGIso.Movement;
using UnityEngine;

namespace RPGIso.Control {
    public class EnemyController : MonoBehaviour {
        [SerializeField] private float targetDetectionDistance = 3f;
        [SerializeField] private float targetInvestigationTime = 3f;
        
        private CombatController combatController;
        private Health health;
        private MovementController movementController;
        private ActionScheduler actionScheduler;
        private GameObject player;

        // TODO split out enemy behavior types
        private Vector3 guardPosition;
        private float timeSinceLastTarget = float.MaxValue;

        private void Start() {
            combatController = GetComponent<CombatController>();
            health = GetComponent<Health>();
            movementController = GetComponent<MovementController>();
            actionScheduler = GetComponent<ActionScheduler>();
            // TODO don't use tags
            player = GameObject.FindWithTag("Player");
            
            guardPosition = transform.position;
        }
        
        private void Update() {
            if (!health.isAlive) {
                return;
            }
            
            if (IsPlayerInRange() && combatController.CanAttack(player)) {
                HandleAttack();
            } else if (timeSinceLastTarget < targetInvestigationTime) {
                HandleInvestigateTarget();
            } else {
                HandleReturnToGuardPosition();
            }
            timeSinceLastTarget += Time.deltaTime;
        }

        private void HandleAttack() {
            timeSinceLastTarget = 0;
            combatController.Attack(player);
        }

        private void HandleInvestigateTarget() {
            actionScheduler.CancelCurrentAction();
        }

        private void HandleReturnToGuardPosition() {
            combatController.CancelAction();
            movementController.MoveTo(guardPosition);
        }

        private bool IsPlayerInRange() {
            return Vector3.Distance(transform.position, player.transform.position) < targetDetectionDistance;
        }
        
        // Can use OnDrawGizmosSelected
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, targetDetectionDistance);
        }
    }
}
