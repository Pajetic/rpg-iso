using UnityEngine;

namespace RPGIso.Combat {
    public class Health : MonoBehaviour {
        [SerializeField] private float health = 50f;

        public void TakeDamage(float damage) {
            health = Mathf.Max(0f, health - damage);
            Debug.Log(health);
        }
    }
}
