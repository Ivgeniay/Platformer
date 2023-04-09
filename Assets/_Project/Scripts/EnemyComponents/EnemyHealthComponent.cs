using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EnemyComponents
{
    internal class EnemyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int health = 1;

        public UnityEvent OnTakeDamageEvent;
        public UnityEvent OnDieEvent;

        public void TakeDamage(int damageValue) {
            health -= damageValue;
            OnTakeDamageEvent?.Invoke();

            if (health <= 0) {
                Die();
            }
        }


        private void Die() {
            OnDieEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
