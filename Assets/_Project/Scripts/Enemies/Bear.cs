using Animations;
using Enemies;
using Player;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    internal class Bear : BaseEnemy
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float attackDelayPeriod;
        [SerializeField] private bool canAttack= true;

        private Transform playerTransform;

        private void Awake()
        {
            playerTransform = FindObjectOfType<PlayerMove>().transform;
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (animator.GetBool(AnimationsStrings.IsAttack)) return;

            var playerDistance = (playerTransform.position - transform.position).magnitude;

            if (playerDistance <= agressiveDistance)
                if (canAttack) {
                    canAttack = false;
                    animator.SetBool(AnimationsStrings.IsAttack, true);
                    StartCoroutine(AttackDelay(attackDelayPeriod));
                }
        }


        public override void ResetAttackAnimation(object obj) {
            animator.SetBool(AnimationsStrings.IsAttack, false);
        }
        public void TakeDamage() {
            animator.SetBool(AnimationsStrings.IsAttack, true);
            animator.SetTrigger(AnimationsStrings.TakeDamage);
        }

        private IEnumerator AttackDelay(float attackPeriod) {
            yield return new WaitForSeconds(attackPeriod);
            canAttack = true;
        }
    }
}
