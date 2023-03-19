using UnityEngine;

namespace Enemies
{
    internal class RabbitAttack : MonoBehaviour
    {
        [SerializeField] private Carrot carrot;
        [SerializeField] private Transform spawnPoint;

        public void Attack() {
            Instantiate(carrot, spawnPoint.position, Quaternion.identity);
        }
    }
}
