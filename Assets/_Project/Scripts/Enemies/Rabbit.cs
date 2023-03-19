using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemies
{
    internal class Rabbit : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float agressiveDistance;

        private Transform playerTransform;

        public static int Attack = Animator.StringToHash("isAttack");

        private void Awake()
        {
            playerTransform = FindObjectOfType<PlayerMove>().transform;
            animator = GetComponentInChildren<Animator>();
        }

        private void Update() {
            if (animator.GetBool(Attack)) return;

            var playerDistance = (playerTransform.position - transform.position).magnitude;

            if (playerDistance <= agressiveDistance)
                animator.SetBool(Attack, true);
        }

        public void ResetAttarck() {
            animator.SetBool(Attack, false);
        }
    }
}
