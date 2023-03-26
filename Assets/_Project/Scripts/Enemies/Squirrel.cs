using Animations;
using Enemies;
using Enemies.Bullets;
using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    internal class Squirrel : BaseEnemy
    {
        [SerializeField] private float throwDelay;
        [SerializeField] private Vector3[] vectorForces;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            StartCoroutine(StartThrow(throwDelay));
        }

        private IEnumerator StartThrow(float delay)
        {
            yield return new WaitForSeconds(delay);
            animator.SetTrigger(AnimationsStrings.Attack);
            StartCoroutine(StartThrow(throwDelay));
        }


        public override void ResetAttackAnimation(object obj) {
            if (obj is Acorn acorn) {
                acorn.SetUp(vectorForces[0], 12);
                acorn.Throw();

                if (vectorForces.Length > 1)
                {
                    for(var i = 1;  i < vectorForces.Length; i++)
                    {
                        var acorns2 = Instantiate(acorn);
                        acorns2.SetUp(vectorForces[i], 12);
                        acorns2.Throw();

                    }
                }
            }
        }
    }
}
