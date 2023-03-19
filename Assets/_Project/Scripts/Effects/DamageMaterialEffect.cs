using System.Collections;
using UnityEngine;

namespace Effects
{
    internal class DamageMaterialEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private float fadeDelay;

        private IEnumerator StartEffectRoutine(float fadeDelay) {

            for (float i = fadeDelay; i >= 0; i -= Time.deltaTime) {
                foreach (Renderer renderer in renderers)
                    foreach (Material material in renderer.materials)
                        renderer.material.SetColor("_EmissionColor", new Color(Mathf.Sin(i * 30) * 0.5f + 0.5f, 0, 0, 0));
                        
                yield return null;
            }

            foreach (Renderer r in renderers)
                r.material.SetColor("_EmissionColor", Color.black);
            
        }

        public void GetEffect()
        {
            StartCoroutine(StartEffectRoutine(fadeDelay));
        }
    }
}
