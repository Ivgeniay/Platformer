using Animations;
using Player;
using UnityEngine;

namespace Enemies
{
    internal class Rabbit : BaseEnemy
    {
        [SerializeField] private Animator animator;

        private Transform playerTransform;

        private void Awake()
        {
            playerTransform = FindObjectOfType<PlayerMove>().transform;
            animator = GetComponentInChildren<Animator>();
        }

        private void Update() {
            if (animator.GetBool(AnimationsStrings.IsAttack)) return;

            var playerDistance = (playerTransform.position - transform.position).magnitude;

            if (playerDistance <= agressiveDistance)
                animator.SetBool(AnimationsStrings.IsAttack, true);
        }

        public override void ResetAttackAnimation(object obj) {
            animator.SetBool(AnimationsStrings.IsAttack, false);
        }
    }
}
