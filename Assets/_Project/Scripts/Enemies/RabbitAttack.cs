using UnityEngine;

namespace Enemies
{
    internal class RabbitAttack : MonoBehaviour
    {
        [SerializeField] private Carrot carrot;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Rabbit rabbit;

        private void Awake()
        {
            if (rabbit is null) rabbit = GetComponentInParent<Rabbit>();
        }

        public void Attack() {
            Instantiate(carrot, spawnPoint.position, Quaternion.identity);
            rabbit.ResetAttarck();
        }
    }
}
