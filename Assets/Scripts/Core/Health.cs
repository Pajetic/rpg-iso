using UnityEngine;

namespace RPGIso.Core {
    public class Health : MonoBehaviour {
        [SerializeField] private float health = 50f;
        
        public bool isAlive {get; private set;}

        private void Start() {
            isAlive = true;
        }

        public void TakeDamage(float damage) {
            health = Mathf.Max(0f, health - damage);
            Debug.Log(health);
            if (health <= 0f) {
                HandleDeath();
            }
        }

        private void HandleDeath() {
            if (isAlive) {
                isAlive = false;
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
        }
    }
}
