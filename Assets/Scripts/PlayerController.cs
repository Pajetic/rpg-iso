using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    private const string MOVE_SPEED_STRING = "MoveSpeed";
    
    [SerializeField] private Transform target;
    
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private Ray lastRay;
    
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            MoveToCursor();
        }
        UpdateAnimator();
    }
    
    private void MoveToCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            navMeshAgent.destination = hit.point;
        }
    }

    private void UpdateAnimator() {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        animator.SetFloat(MOVE_SPEED_STRING, localVelocity.magnitude);
    }
}
