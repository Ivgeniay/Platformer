using UnityEngine;

namespace Utilits
{
    internal class TimeManager : MonoBehaviour
    {
        [SerializeField] private float timeScale = 0.2f;
        private float startTimeScale = 0;
        private float startFixetTimestep = 0;
        private bool isPause;

        private void Awake()
        {
            startTimeScale = Time.timeScale;
            startFixetTimestep = Time.fixedDeltaTime;
        }

        private void Update()
        {
            if (isPause) return;

            if (Input.GetMouseButton(1)) {
                Time.timeScale = timeScale;
                Time.fixedDeltaTime = Time.timeScale * startFixetTimestep;
            }
            else {
                Time.timeScale = startTimeScale;
                Time.fixedDeltaTime = startFixetTimestep;
            }
        }

        public void Pause() {
            isPause = true;
            Time.timeScale = 0.01f;
            Time.fixedDeltaTime = Time.timeScale * startFixetTimestep;
        }

        public void UnPause() {
            isPause = false;
        }

        private void OnDestroy() {
            Time.timeScale = startTimeScale;
            Time.fixedDeltaTime = startFixetTimestep;
        }
    }
}
