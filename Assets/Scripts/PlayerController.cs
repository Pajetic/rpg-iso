using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform target;
    
    NavMeshAgent navMeshAgent;
    
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (navMeshAgent != null) {
            navMeshAgent.destination = target.position;
        }
    }
}
