using Player;
using UnityEngine;

namespace EnemyComponents
{
    internal class MakeDamageOnCollisionComponent : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody)
            {
                var playerHelth = collision.rigidbody.GetComponent<PlayerHealth>();
                if (playerHelth)
                {
                    playerHelth.TakeDamage(damage);
                }
            }
        }


    }
}
