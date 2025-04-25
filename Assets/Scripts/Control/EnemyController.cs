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
        // Guard
        private Vector3 guardPosition;
        private float timeSinceLastTarget = float.MaxValue;
        
        // Patrol
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float patrolPointTolerance = 0.05f;
        [SerializeField] private float patrolPointWaitTime = 3f;
        private int currentPatrolPointIndex = 0;
        private float timeSincePatrolPointArrival = float.MaxValue;
        
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
            } else if (patrolPath != null) {
                HandlePatrol();
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

        private void HandlePatrol() {
            if (Vector3.Distance(transform.position, patrolPath.GetPatrolPosition(currentPatrolPointIndex)) <
                patrolPointTolerance) {
                currentPatrolPointIndex = patrolPath.GetNextPatrolPointIndex(currentPatrolPointIndex);
                timeSincePatrolPointArrival = 0;
            }
            timeSincePatrolPointArrival += Time.deltaTime;
            if (timeSincePatrolPointArrival > patrolPointWaitTime) {
                movementController.MoveTo(patrolPath.GetPatrolPosition(currentPatrolPointIndex));
            }
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
