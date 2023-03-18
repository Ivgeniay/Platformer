using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float destroyDelay;
    private void Awake() {
        Destroy(gameObject, destroyDelay);
    }

}
