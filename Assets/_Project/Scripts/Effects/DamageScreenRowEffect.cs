using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Effects
{
    internal class DamageScreenRowEffect : MonoBehaviour, IEffect
    {
        [SerializeField] private Image image;
        [SerializeField] private float fadeDelay;

        private IEnumerator StartEffectRoutine(float fadeDelay) {
            image.enabled = true;

            for (float i = fadeDelay; i >= 0; i -= Time.deltaTime)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.InverseLerp(0, fadeDelay, i));
                yield return null;
            }

            image.enabled = false;
        }

        public void StartEffect()
        {
            StartCoroutine(StartEffectRoutine(fadeDelay));
        }
    }
}
