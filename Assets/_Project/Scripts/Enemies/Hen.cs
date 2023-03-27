using Player;
using UnityEngine;

namespace Enemies
{
    internal class Hen : BaseEnemy
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed;
        [SerializeField] private float timeToReachSpeed;

        private void Start()
        {
            playerTransform = FindObjectOfType<PlayerMove>().transform;
        }

        private void FixedUpdate()
        {
            Vector3 toPlayer = playerTransform.position - transform.position;
            toPlayer = toPlayer.normalized;

            if (toPlayer.magnitude < agressiveDistance) {
                Vector3 force = rb.mass * (toPlayer * speed - rb.velocity)/timeToReachSpeed;
                rb.AddForce(force);
            }

        }

        public override void ResetAttackAnimation(object obj)
        { }
    }
}
