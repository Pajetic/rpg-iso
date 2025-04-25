using RPGIso.Combat;
using RPGIso.Core;
using UnityEngine;

namespace RPGIso.Control {
    public class EnemyController : MonoBehaviour {
        [SerializeField] private float chaseDistance = 3f;
        
        private CombatController combatController;
        private Health health;
        private GameObject player;

        private void Start() {
            combatController = GetComponent<CombatController>();
            health = GetComponent<Health>();
            // TODO don't use tags
            player = GameObject.FindWithTag("Player");
        }
        
        private void Update() {
            if (!health.isAlive) {
                return;
            }
            
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
