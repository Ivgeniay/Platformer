using Player;
using UnityEngine;

namespace Enemies
{
    internal class Carrot : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            var player = FindObjectOfType<PlayerMove>().transform;
            Vector3 toPlayer = (player.position + new Vector3(0,1,0) - transform.position).normalized;

            rb.velocity = toPlayer * speed;
        }
    }
}
