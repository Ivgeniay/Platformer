using UnityEngine;

namespace Gun
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] destroyEffect;
        [SerializeField] private float lifeTime = 2f;
        public bool isLast { get; private set; }
        public int Damage { get; private set; }
        
        private void Awake() {
            Destroy(gameObject, lifeTime);
        }
        public void SetUp(int damage, bool isLast = false) {
            Damage = damage;
            this.isLast = isLast;
        }

        private void OnCollisionEnter(Collision collision) {
            DestroyBullet();
        }

        public void DestroyBullet()
        {
            var num = Random.Range(0, destroyEffect.Length);
            Instantiate(destroyEffect[num], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
