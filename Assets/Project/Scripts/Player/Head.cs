using UnityEngine;

namespace Player
{
    public class Head : MonoBehaviour
    {
        [SerializeField] private Transform target;

        void Update() =>        
            transform.position = target.position;
        
    }
}
