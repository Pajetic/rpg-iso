using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform target;
    
    NavMeshAgent navMeshAgent;

    private Ray lastRay;
    
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            MoveToCursor();
        }
    }
    
    private void MoveToCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            navMeshAgent.destination = hit.point;
        }
    }
}
