using Enemies;
using Enemies.Bullets;
using UnityEngine;

namespace EnemyComponents
{
    internal class AttackAnimationProxyComponent : MonoBehaviour
    {
        [SerializeField] private BaseEnemyBullet enemyBullet;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private BaseEnemy enemy;

        private void Awake()
        {
            if (enemy is null) enemy = GetComponentInParent<BaseEnemy>();
        }

        public void Attack() {
            var bullet = Instantiate(enemyBullet, new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
            enemy.ResetAttackAnimation(bullet);
        }
    }
}
