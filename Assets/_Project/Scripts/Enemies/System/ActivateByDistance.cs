using Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Enemies.System
{
    public class ActivateByDistance : MonoBehaviour
    {

        [SerializeField] private float distanceToActivate;
        [SerializeField] private float delayUpdate;
        [SerializeField] private bool isJob;
        private PlayerMove playerMove;
        private List<BaseEnemy> enemies;


        private void Start()
        {
            playerMove = GameObject.FindObjectOfType<PlayerMove>();
            enemies = GameObject.FindObjectsOfType<BaseEnemy>().ToList();

            Activate(distanceToActivate);
        }

        private void Update() {
            enemies.RemoveAll(enemy => enemy.IsNotExist());

            if (isJob)
            {
                NativeArray<Vector3> enemyPositions = new NativeArray<Vector3>(enemies.Count, Allocator.TempJob);
                for(int i = 0; i < enemyPositions.Length; i++) {
                    enemyPositions[i] = enemies[i].transform.position;
                }

                NativeArray<Vector3> playerPositions = new NativeArray<Vector3>(1, Allocator.TempJob);
                playerPositions[0] = playerMove.transform.position;

                NativeArray<float> minDistance = new NativeArray<float>(1, Allocator.TempJob);
                minDistance[0] = distanceToActivate;

                NativeArray<bool> isActive = new NativeArray<bool>(enemies.Count, Allocator.TempJob);

                SeekAndActivate seekAndActivate = new SeekAndActivate() {
                    enemyPosition = enemyPositions, 
                    playerPosition = playerPositions,
                    minDistance = minDistance,
                    isActive = isActive
                };

                var handle = seekAndActivate.Schedule(enemyPositions.Length-1, 4);
                handle.Complete();

                for(int i =0; i < enemies.Count; i++) 
                    enemies[i].gameObject.SetActive(seekAndActivate.isActive[i]);
                

                enemyPositions.Dispose();
                playerPositions.Dispose();
                minDistance.Dispose();
                isActive.Dispose();
            }
            else
            {
                Activate(distanceToActivate);
            }
        }

        public void Activate(float minDistanceToActivate) {
            enemies.ForEach(enemy => {
                var distance = (enemy.transform.position - playerMove.transform.position).magnitude;
                if (distance < minDistanceToActivate)
                    enemy.gameObject.SetActive(true);
                else
                    enemy.gameObject.SetActive(false);
            });
        }

    public IEnumerator StartUpdate(float delay, float minDistanceToActivate)
        {
            yield return new WaitForSeconds(delay);
            Activate(minDistanceToActivate);
        }

    }


    [BurstCompile]
    internal struct SeekAndActivate : IJobParallelFor
    {
        public NativeArray<Vector3> playerPosition;
        public NativeArray<Vector3> enemyPosition;
        public NativeArray<float> minDistance;
        public float _minDistance;
        public NativeArray<bool> isActive;

        public void Execute(int index)
        {
            var div = enemyPosition[index] - playerPosition[0];
            var distance = Mathf.Sqrt(div.x* div.x + div.y * div.y + div.z * div.z); // sqrt(x*x+y*y+z*z)
            //var distance = div.magnitude;

            isActive[index] = distance < minDistance[0];
        }
    }

    internal static class ExtensionObjects
    {
        public static bool IsNull(this object self) =>
            self is null;
        public static bool IsNotExist(this UnityEngine.Object target) =>
            target == null;
        public static bool IsExist(this UnityEngine.Object target) =>
            target != null;
    }

}

