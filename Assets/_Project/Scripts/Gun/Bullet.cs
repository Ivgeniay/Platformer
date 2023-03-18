using UnityEngine;

namespace Gun
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] destroyEffect;
        [SerializeField] private float lifeTime = 2f;
        
        private void Awake() {
            Destroy(gameObject, lifeTime);
        }

        private void OnCollisionEnter(Collision collision) {
            var num = Random.Range(0, destroyEffect.Length);
            Instantiate(destroyEffect[num], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
