using Player;
using UnityEngine;

namespace Enemies.Bullets
{
    internal class Rocket : BaseEnemyBullet
    {
        [SerializeField] private float speed;
        [SerializeField] private float speedRotation;

        private Rigidbody rbody;
        private Transform playerTransform;

        private void Awake() {
            playerTransform = FindObjectOfType<PlayerMove>().transform;
            rbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 toPlayer = playerTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            rbody.position += transform.forward * speed * Time.deltaTime;
        }

    }
}
