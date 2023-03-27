using UnityEngine;
using UnityEngine.Events;

namespace EnemyComponents
{
    internal class WalkerComponent : MonoBehaviour
    {
        [SerializeField] private Transform leftTarget;
        [SerializeField] private Transform rightTarget;

        [SerializeField] private float speed;
        [SerializeField] private float speedRotation;

        [SerializeField] private float stopTime;
        [SerializeField] private Transform raycastPosition;

        private Direction currentDirection;
        private bool isStopped;

        public UnityEvent OnRightTargetEvent;
        public UnityEvent OnLeftTargetEvent;



        private void Awake()
        {
            leftTarget.parent = null;
            rightTarget.parent = null;
        }
        private void Update()
        {
            if (isStopped == true) return;

            if (currentDirection == Direction.Left) {
                transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
                if (transform.position.x < leftTarget.position.x) {
                    currentDirection = Direction.Right;
                    isStopped = true;
                    Invoke("ContinueWalk", stopTime);
                    OnLeftTargetEvent?.Invoke();
                }
                
            
            }
            else {
                transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
                if (transform.position.x > rightTarget.position.x) {
                    currentDirection = Direction.Left;
                    isStopped = true;
                    Invoke("ContinueWalk", stopTime);
                    OnRightTargetEvent?.Invoke();
                }
            }

            RaycastHit hit;
            if(Physics.Raycast(raycastPosition.position, Vector3.down, out hit)) {
                transform.position = hit.point;
            }
        }

        private void ContinueWalk() =>         
            isStopped = false;
        
    }
}

public enum Direction
{
    Left,
    Right
}