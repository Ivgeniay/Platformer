using Gun;
using UnityEngine;

namespace EnemyComponents
{
    [RequireComponent(typeof(EnemyHealthComponent))]
    internal class TakeDamageOnTriggerComponent : MonoBehaviour
    {
        [SerializeField] EnemyHealthComponent healthComponent;

        private void Awake()
        {
            if (!healthComponent) healthComponent = GetComponent<EnemyHealthComponent>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.attachedRigidbody)
            {
                var bullt = collision.attachedRigidbody.GetComponent<Bullet>();//collision.gameObject.GetComponentInParent<Bullet>();
                if (bullt)
                {
                    healthComponent.TakeDamage(bullt.Damage);
                    bullt.DestroyBullet();
                }
            }
        }
    }
}
