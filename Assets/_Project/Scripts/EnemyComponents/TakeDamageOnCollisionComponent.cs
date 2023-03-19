using GameItems;
using Gun;
using UnityEngine;

namespace EnemyComponents
{
    [RequireComponent(typeof(EnemyHealthComponent))]
    internal class TakeDamageOnCollisionComponent : MonoBehaviour
    {
        [SerializeField] EnemyHealthComponent healthComponent;
        [SerializeField] private bool DieOnAnyCollision;

        private void Awake() {
            if (!healthComponent) healthComponent = GetComponent<EnemyHealthComponent>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody) {
                var bullt = collision.gameObject.GetComponent<Bullet>();
                if (bullt) {
                    healthComponent.TakeDamage(bullt.Damage);
                }
            }    

            if (DieOnAnyCollision && collision.gameObject.GetComponent<ILoot>() == null)
                healthComponent.TakeDamage(10000);
        }
    }
}
