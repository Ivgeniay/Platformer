using PlayerInput;
using Sounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilits;

namespace Scripts.UI
{
    internal class MenuMediator : MonoBehaviour
    {
        [SerializeField] private GameObject menuButton;
        [SerializeField] private GameObject menuLayout;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private SoundManager soundManager;

        public void OpenMenuWindow() {
            InputSys.Instance.SetInputActive(false);

            timeManager.Pause();
            menuButton.SetActive(false);
            menuLayout.SetActive(true);
        }

        public void CloseMenuWindow() {
            InputSys.Instance.SetInputActive(true);

            timeManager.UnPause();
            menuButton.SetActive(true);
            menuLayout.SetActive(false);
        }

        public void Restart() {
            InputSys.Instance.SetInputActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Volume(float value) =>
            soundManager.SetVolume(value);
        

        public void SetMusic(bool value) =>
            soundManager.SetMusicEnabled(value);
    }
}
