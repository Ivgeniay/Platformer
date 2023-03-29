using UnityEngine;

namespace Utilits
{
    internal class TimeManager : MonoBehaviour
    {
        [SerializeField] private float timeScale = 0.2f;
        private float startTimeScale = 0;
        private float startFixetTimestep = 0;

        private void Awake()
        {
            startTimeScale = Time.timeScale;
            startFixetTimestep = Time.fixedDeltaTime;
        }

        private void Update()
        {
            if (Input.GetMouseButton(1)) {
                Time.timeScale = timeScale;
                Time.fixedDeltaTime = Time.timeScale * startFixetTimestep;
            }
            else {
                Time.timeScale = startTimeScale;
                Time.fixedDeltaTime = startFixetTimestep;
            }
        }
    }
}
