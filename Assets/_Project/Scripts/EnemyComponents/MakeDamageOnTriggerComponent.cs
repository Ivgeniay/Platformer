using Player;
using UnityEngine;

namespace EnemyComponents
{
    internal class MakeDamageOnTriggerComponent : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnTriggerEnter(Collider other)
        { 
            var playerHelth = other.GetComponentInParent<PlayerHealth>();
            if (playerHelth) {
                playerHelth.TakeDamage(damage);
            }
            
        }
    }
}
