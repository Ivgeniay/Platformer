using Enemies.Bullets;
using UnityEngine;

namespace Enemies.Bullets
{
    internal class Acorn : BaseEnemyBullet
    {
        private Rigidbody rigidb;
        private Vector3 velocity;
        private float maxRotationSpeed = 2;

        private void Awake() {
            rigidb = GetComponent<Rigidbody>();
        }

        public void SetUp(Vector3 velocity, float maxRorationSpeed = 1) {
            this.velocity = velocity;
        }

        public void Throw() {
            rigidb.AddRelativeForce(velocity, ForceMode.VelocityChange);
            rigidb.angularVelocity = new Vector3(
                Random.Range(-maxRotationSpeed, maxRotationSpeed),
                Random.Range(-maxRotationSpeed, maxRotationSpeed),
                Random.Range(-maxRotationSpeed, maxRotationSpeed)
                );
        }
    }
}
