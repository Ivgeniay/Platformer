using Effects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    internal class PlayerHealth : MonoBehaviour
    {

        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        [SerializeField] private float damageCooldown;
        [SerializeField] private AudioSource addHealthSound;

        private bool canTakeDamage = true;

        public UnityEvent OnTakeDamageEvent;

        public void TakeDamage(int damageValue)
        {
            if (health <= 0) return;
            if (!canTakeDamage) return;

            StartCoroutine(CooldownTimer(damageCooldown));

            OnTakeDamageEvent?.Invoke();

            health -= damageValue;
            if (health <= 0) {
                health = 0;
                Die();
            } 
        }

        public void AddHealth(int healthValue) {
            health += healthValue;
            if (health >= maxHealth) {
                health = maxHealth;
            }
            addHealthSound.Play();
        }

        private void Die()
        {
            Debug.Log("Die");
            //Destroy(this.gameObject);
        }

        private IEnumerator CooldownTimer(float delay)
        {
            canTakeDamage = false;
            yield return new WaitForSeconds(delay);
            canTakeDamage = true;
        }
    }
}
