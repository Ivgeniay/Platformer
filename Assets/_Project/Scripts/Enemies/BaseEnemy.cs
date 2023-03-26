using UnityEngine;

namespace Enemies
{
    internal abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected float agressiveDistance;
        public abstract void ResetAttackAnimation(object obj);

    }
}
