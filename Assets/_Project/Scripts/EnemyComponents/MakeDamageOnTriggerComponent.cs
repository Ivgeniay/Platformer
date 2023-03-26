using Player;
using UnityEngine;

namespace EnemyComponents
{
    internal class MakeDamageOnTriggerComponent : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnTriggerEnter(Collider other)
        { 
            if (other.attachedRigidbody)
            {
                var playerHelth = other.attachedRigidbody.GetComponent<PlayerHealth>(); //other.GetComponentInParent<PlayerHealth>();
                if (playerHelth) {
                    playerHelth.TakeDamage(damage);
                }
            }
            
        }
    }
}
