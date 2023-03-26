using Player;
using UnityEngine;

namespace Enemies.Bullets
{
    internal class Carrot : BaseEnemyBullet
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxLifetime = 5;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            var player = FindObjectOfType<PlayerMove>().transform;
            Vector3 toPlayer = (player.position + new Vector3(0,1,0) - transform.position).normalized;

            rb.velocity = toPlayer * speed;

            Destroy(gameObject, maxLifetime);
        }
    }
}
