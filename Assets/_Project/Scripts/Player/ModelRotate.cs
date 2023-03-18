using PlayerInput;
using UnityEngine;

namespace Player
{
    public class ModelRotate : MonoBehaviour
    {
        [SerializeField] private float angel;
        [SerializeField] private float speedRotating;

        private Vector3 mousePosition;

        private void OnEnable() {
            //Subscribe();
        }
        private void OnDisable() {
            //Unsubscribe();
        }

        private void Update() {
            mousePosition = InputSys.Instance.mouseWorldPosition.Value;

            if (mousePosition.x > transform.position.x)
                transform.rotation =
                    Quaternion.Lerp(
                        transform.rotation,
                        Quaternion.Euler(0, -angel, 0),
                        Time.deltaTime * speedRotating
                        );
            else
                transform.rotation =
                    Quaternion.Lerp(
                        transform.rotation,
                        Quaternion.Euler(0, angel, 0),
                        Time.deltaTime * speedRotating
                        );
        }

        //private void OnMouseWorldPositionChangedHandler(Vector3 obj) =>
        //    mousePosition = obj;
        //private void Subscribe() {
        //    InputSys.Instance.mouseWorldPosition.OnValueChangedEvent += OnMouseWorldPositionChangedHandler;
        //}
        //private void Unsubscribe() {
        //    InputSys.Instance.mouseWorldPosition.OnValueChangedEvent -= OnMouseWorldPositionChangedHandler;
        //}

    }
}
