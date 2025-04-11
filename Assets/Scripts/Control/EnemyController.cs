using RPGIso.Combat;
using UnityEngine;

namespace RPGIso.Control {
    public class EnemyController : MonoBehaviour {
        [SerializeField] private float chaseDistance = 3f;
        
        CombatController combatController;
        GameObject player;

        private void Start() {
            combatController = GetComponent<CombatController>();
            // TODO don't use tags
            player = GameObject.FindWithTag("Player");
        }
        
        private void Update() {
            if (IsPlayerInRange() && combatController.CanAttack(player)) {
                combatController.Attack(player);
            } else {
                combatController.CancelAction();
            }
        }

        private bool IsPlayerInRange() {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }
    }
}
