using Player;
using UnityEngine;

namespace GameItems.LootHeal
{
    internal class LootHeal : MonoBehaviour, ILoot
    {
        [SerializeField] private int healthValue;
        private bool isCollided;

        private void OnTriggerEnter(Collider other)
        {
            if (isCollided) return;

            var player = other.attachedRigidbody.GetComponent<PlayerHealth>();
            if (player)
            {
                isCollided = true;
                player.AddHealth(healthValue);
                Die();
            }
        }

        private void Die() {
            Destroy(gameObject);
        }
    }
}
