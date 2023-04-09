using UnityEngine;

namespace Rope
{
    internal class RopeRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private int Segments = 10;

        public void Draw(Vector3 a, Vector3 b, float length)
        {
            lineRenderer.enabled = true;

            float interpolant = Vector3.Distance(a, b) / length;
            float offset = Mathf.Lerp(length / 2f, 0f, interpolant);

            Vector3 aDown = a + Vector3.down * offset;
            Vector3 bDown = b + Vector3.down * offset;

            lineRenderer.positionCount = Segments + 1;
            for (int i = 0; i < lineRenderer.positionCount; i++) {
                lineRenderer.SetPosition(i, Bezier.GetPoint(a, aDown, bDown, b, (float)i / Segments));
            }
        }

        public void Hide()
        {
            lineRenderer.enabled = false;
        }
    }
}
