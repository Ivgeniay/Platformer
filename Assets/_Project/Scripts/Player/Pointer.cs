using PlayerInput;
using UnityEngine;

namespace Player
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private Transform aimTransform;

        private Vector3 mousePosition;

        private void OnEnable() {
            Subscribe();
        }
        private void OnDisable() {
            Unsubscribe();
        }

        private void Update() {
            mousePosition = InputSys.Instance.mouseWorldPosition.Value;
        }

        private void LateUpdate() {
            aimTransform.position = mousePosition;

            var toAimDirection = aimTransform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(toAimDirection);
        }
        private void Subscribe() {
            InputSys.Instance.mouseWorldPosition.OnValueChangedEvent += OnMouseWorldPositionChangedHandler;
        }
        private void Unsubscribe() {
            InputSys.Instance.mouseWorldPosition.OnValueChangedEvent -= OnMouseWorldPositionChangedHandler;
        }

        private void OnMouseWorldPositionChangedHandler(Vector3 obj) =>
            mousePosition = obj;
        

    }
}
