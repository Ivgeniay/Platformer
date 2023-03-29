using UnityEngine;

namespace Sounds
{
    internal class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void SetMusicEnabled(bool value) =>
            audioSource.enabled = value;

        public void SetVolume(float value) =>
            AudioListener.volume = value;
    }
}
