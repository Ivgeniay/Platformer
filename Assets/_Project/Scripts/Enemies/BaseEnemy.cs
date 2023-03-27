using UnityEngine;

namespace Enemies
{
    internal abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected float agressiveDistance;
        protected Transform playerTransform;
        public abstract void ResetAttackAnimation(object obj);

    }
}
