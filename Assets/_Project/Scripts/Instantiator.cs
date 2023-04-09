using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;

    public void Create() {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
