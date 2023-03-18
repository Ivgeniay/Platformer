using PlayerInput;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class EyesRotate : MonoBehaviour
    {
        [SerializeField] private float maxAngle = 30;
        [SerializeField] private Transform relativeTransorm;

        private float reverseAngle;
        private Vector3 startRotation;

        private void Awake() {
            startRotation = transform.localEulerAngles;
            reverseAngle = 180 - maxAngle;
        }

        private void OnEnable() {
            Subscribe();
        }
        private void OnDisable() {
            Unsubscribe();
        }
        private void OnMouseWorldPositionChangedHandler(Vector3 obj)
        {
            var v1 = relativeTransorm.position - obj;
            var v2 = Vector3.right;

            float angle = Vector3.Angle(v1, v2);

            if (angle > reverseAngle)
                angle = 180 - angle;

            if (angle < maxAngle) {
                if (obj.y < relativeTransorm.position.y && angle < maxAngle)
                    transform.localEulerAngles = startRotation + new Vector3(angle, 0, 0);
                else if (obj.y > relativeTransorm.position.y && angle < maxAngle)
                    transform.localEulerAngles = startRotation - new Vector3(angle, 0, 0);
            }
        }
        private void Subscribe() {
            InputSys.Instance.mouseWorldPosition.OnValueChangedEvent += OnMouseWorldPositionChangedHandler;
        }
        private void Unsubscribe() {
            InputSys.Instance.mouseWorldPosition.OnValueChangedEvent -= OnMouseWorldPositionChangedHandler;
        }

    }
}
