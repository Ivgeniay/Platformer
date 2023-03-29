using Player;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Utilits;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Enemies.System
{
    public class ActivateByDistance : MonoBehaviour
    {

        [SerializeField] private float distanceToActivate;
        [SerializeField] private bool isJob;
        private PlayerMove playerMove;
        [SerializeField]private List<BaseEnemy> enemies;


        private void Start()
        {
            playerMove = GameObject.FindObjectOfType<PlayerMove>();
            enemies = GameObject.FindObjectsOfType<BaseEnemy>().ToList();

            Activate(distanceToActivate);
        }

        private void Update() {
            enemies.RemoveAll(enemy => enemy.IsNotExist());

            if (isJob) {
                NativeArray<Vector3> enemyPositions = new NativeArray<Vector3>(enemies.Count, Allocator.TempJob);
                for(int i = 0; i < enemyPositions.Length; i++) 
                    enemyPositions[i] = enemies[i].transform.position;
                
                NativeArray<bool> isActive = new NativeArray<bool>(enemies.Count, Allocator.TempJob);

                SeekAndActivate seekAndActivate = new SeekAndActivate() {
                    enemyPosition = enemyPositions,
                    _playerPosition = playerMove.transform.position,
                    minDistance = distanceToActivate,
                    isActive = isActive
                };

                var handle = seekAndActivate.Schedule(enemyPositions.Length-1, 100);
                handle.Complete();

                for(int i =0; i < enemies.Count; i++) 
                    enemies[i].gameObject.SetActive(seekAndActivate.isActive[i]);
                

                enemyPositions.Dispose();
                isActive.Dispose();
            }
            else
                Activate(distanceToActivate);
            
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            enemies = GameObject.FindObjectsOfType<BaseEnemy>().ToList();

            enemies.ForEach(el =>
            {
                Handles.color = Color.yellow;
                Handles.DrawWireDisc(el.transform.position, -Vector3.forward, distanceToActivate);
            });
        }

#endif


        public void Activate(float minDistanceToActivate) {
            enemies.ForEach(enemy => {
                var distance = (enemy.transform.position - playerMove.transform.position).magnitude;
                enemy.gameObject.SetActive(distance < minDistanceToActivate);

                //Debug.Log($"Factorial 10: {GetFactorial(10)}");
                
            });
        }

        //private int GetFactorial(int f) {
        //    if (f == 0)
        //        return 1;
        //    return f * GetFactorial(f - 1);
        //}

    }


    [BurstCompile]
    internal struct SeekAndActivate : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<Vector3> enemyPosition;
        [ReadOnly]
        public Vector3 _playerPosition;
        [ReadOnly]
        public float minDistance;
        [WriteOnly]
        public NativeArray<bool> isActive;

        public void Execute(int index)
        {
            var div = enemyPosition[index] - _playerPosition;
            var distance = Mathf.Sqrt(div.x* div.x + div.y * div.y + div.z * div.z);

            isActive[index] = distance < minDistance;
            //Debug.Log($"Factorial 10: {GetFactorial(10)}");
        }

        //private int GetFactorial(int f)
        //{
        //    if (f == 0)
        //        return 1;
        //    return f * GetFactorial(f - 1);
        //}
    }

}

