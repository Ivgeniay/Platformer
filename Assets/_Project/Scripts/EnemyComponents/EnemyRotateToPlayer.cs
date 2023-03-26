using Player;
using UnityEngine;

namespace EnemyComponents
{
    internal class EnemyRotateToPlayer : MonoBehaviour
    {
        [SerializeField] private Vector3 leftEuler;
        [SerializeField] private Vector3 rightEuler;
        [SerializeField] private float speedRotarion = 5f;

        private Vector3 targetEuler;
        private Transform playerTransform;

        private void Awake()
        {
            playerTransform = FindObjectOfType<PlayerMove>().transform;
        }

        private void Update()
        {
            if (transform.position.x < playerTransform.position.x) {
                targetEuler = rightEuler;
            }
            else {
                targetEuler = leftEuler;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetEuler), Time.deltaTime * speedRotarion);
        }

    }
}
