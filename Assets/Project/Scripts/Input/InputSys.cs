using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilit;

namespace PlayerInput
{
    public class InputSys : MonoBehaviour
    {
        public event Action OnJumpEvent;
        public event Action OnShootEvent;
        public ObservableVariable<Vector3> mouseWorldPosition = new ObservableVariable<Vector3>();

        private Actions inputActions;

        private static InputSys instance = null;
        public static InputSys Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<InputSys>();
                    if (instance == null ) {
                        var go = new GameObject("[INPUTSYSTEM]");
                        instance = go.AddComponent<InputSys>();
                        DontDestroyOnLoad(go);
                    }
                }
                return instance;
            }
        }
        private Plane plane;
        private Camera currentCamera;

        #region Mono
        private void Awake() {
            plane = new Plane(-Vector3.forward, Vector3.zero);
            currentCamera = Camera.main;
            inputActions = new Actions();
        }
        private void OnEnable() {
            inputActions.Enable();
            inputActions.FPS.Jump.performed += JumpPerformed;
            inputActions.FPS.Shoot.performed += ShootPerformed;
        }
        private void OnDisable() {
            inputActions.FPS.Jump.performed -= JumpPerformed; ;
            inputActions.FPS.Shoot.performed -= ShootPerformed;
            inputActions.Disable();
        }
        void Update() {
            var mousePos = GetMousePosition();

            Ray ray = currentCamera.ScreenPointToRay(mousePos);
            plane.Raycast(ray, out float distance);
            mouseWorldPosition.Value = ray.GetPoint(distance);
        }
        #endregion

        public Vector2 GetMoving() =>
            inputActions.FPS.Mooving.ReadValue<Vector2>();

        private Vector2 GetMousePosition() =>
            inputActions.FPS.MousePosition.ReadValue<Vector2>();
        private void JumpPerformed(InputAction.CallbackContext obj) =>
            OnJumpEvent?.Invoke();
        private void ShootPerformed(InputAction.CallbackContext obj) =>
            OnShootEvent?.Invoke();

    }
}