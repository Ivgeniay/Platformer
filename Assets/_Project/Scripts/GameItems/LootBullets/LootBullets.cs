using Player;
using UnityEngine;

namespace GameItems.LootBullets
{
    internal class LootBullets : MonoBehaviour, ILoot
    {

        [SerializeField] private int gunIndex;
        [SerializeField] private int Amount;
        private bool isCollided;

        private void OnTriggerEnter(Collider other)
        {
            if (isCollided) return;

            var player = other.attachedRigidbody.GetComponent<PlayerGunplay>();
            if (player) {
                isCollided = true;
                player.GetGuns()[gunIndex].AddBullets(Amount);
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

    }
}
