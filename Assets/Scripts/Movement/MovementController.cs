using RPGIso.Combat;
using RPGIso.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPGIso.Movement {
    public class MovementController : MonoBehaviour, IAction {
        private const string MOVE_SPEED_STRING = "MoveSpeed";
    
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private Health health;
    
        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = gameObject.GetComponent<Health>();
        }

        private void Update() {
            navMeshAgent.enabled = health.isAlive;
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination) {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
    
        public void MoveTo(Vector3 destination) {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void CancelAction() {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator() {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            animator.SetFloat(MOVE_SPEED_STRING, localVelocity.magnitude);
        }
    }
}
