using System.Collections;
using UnityEngine;

namespace Effects
{
    internal class DamageMaterialEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private float fadeDelay;

        private void OnEnable() {
            SerColor(Color.clear);
        }

        private IEnumerator StartEffectRoutine(float fadeDelay) {

            for (float i = fadeDelay; i >= 0; i -= Time.deltaTime) {
                SerColor(new Color(Mathf.Sin(i * 30) * 0.5f + 0.5f, 0, 0, 0));
                yield return null;
            }

            SerColor(Color.clear);
        }

        public void StartEffect() {
            StartCoroutine(StartEffectRoutine(fadeDelay));
        }

        private void SerColor(Color color) {
            foreach (Renderer renderer in renderers)
                foreach (Material material in renderer.materials)
                    material.SetColor("_EmissionColor", color);
        }
    }
}
