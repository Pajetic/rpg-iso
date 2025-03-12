using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour {
    private const string MOVE_SPEED_STRING = "MoveSpeed";
    
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        UpdateAnimator();
    }
    
    public void MoveTo(Vector3 destination) {
        navMeshAgent.destination = destination;
    }

    private void UpdateAnimator() {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        animator.SetFloat(MOVE_SPEED_STRING, localVelocity.magnitude);
    }
}
