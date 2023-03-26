using UnityEngine;

namespace Animations
{
    internal static class AnimationsStrings
    {
        public static int IsAttack { get; private set; } = Animator.StringToHash("isAttack");
        public static int Attack { get; private set; } = Animator.StringToHash("Attack");
        public static int TakeDamage { get; private set; } = Animator.StringToHash("TakeDamage");
    }
}
