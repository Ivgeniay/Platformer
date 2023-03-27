using UnityEngine;

namespace EnemyComponents
{
    internal class RotateToTargetEulerComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 LeftEuler;
        [SerializeField] private Vector3 RightEuler;

        [SerializeField] private float rotateSpeed;
        private Vector3 targetEuler;

        private void Update()
        {
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.Euler(targetEuler),
                Time.deltaTime * rotateSpeed
                );
        }

        public void RotateLeft() {
            targetEuler = LeftEuler;
        }

        public void RotateRight() { 
            targetEuler = RightEuler;
        }

    }
}
