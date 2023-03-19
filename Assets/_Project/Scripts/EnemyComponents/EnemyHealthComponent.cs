using UnityEngine;
using UnityEngine.Events;

namespace EnemyComponents
{
    internal class EnemyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int health = 1;

        public UnityEvent OnTakeDamageEvent;

        public void TakeDamage(int damageValue) {
            health -= damageValue;
            OnTakeDamageEvent?.Invoke();

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die() {
            Destroy(gameObject);
        }
    }
}
