using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilit;

namespace PlayerInput
{
    public class InputSys : MonoBehaviour
    {
        public event Action OnJumpEvent;
        public event Action OnShootingStartedEvent;
        public event Action OnShootingEndedEvent;

        public event Action<int> OnChangeGunEvent;

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
            inputActions = new Actions();
        }
        private void OnEnable() {
            plane = new Plane(-Vector3.forward, Vector3.zero);
            currentCamera = Camera.main;

            inputActions.Enable();
            inputActions.FPS.Jump.performed += JumpPerformed;
            inputActions.FPS.Shoot.performed += ShootPerformed;
            inputActions.FPS.Shoot.canceled += ShootEndPerformed;
            inputActions.FPS.ChangePistol.performed += ChangePistolPerformed;
            inputActions.FPS.ChangeMachineGun.performed += ChangeMachineGunPerformed;
        }

        private void ChangePistolPerformed(InputAction.CallbackContext obj) =>
            OnChangeGunEvent?.Invoke(1);
        private void ChangeMachineGunPerformed(InputAction.CallbackContext obj) =>
            OnChangeGunEvent?.Invoke(2);

        private void OnDisable() {
            inputActions.FPS.Jump.performed -= JumpPerformed; ;
            inputActions.FPS.Shoot.performed -= ShootPerformed;
            inputActions.FPS.Shoot.canceled -= ShootEndPerformed;
            inputActions.FPS.ChangePistol.performed -= ChangePistolPerformed;
            inputActions.FPS.ChangeMachineGun.performed -= ChangeMachineGunPerformed;
            inputActions.Disable();
        }
        void Update() {
            if (currentCamera is null) currentCamera = Camera.main;
            var mousePos = GetMousePosition();

            Ray ray = currentCamera.ScreenPointToRay(mousePos);
            plane.Raycast(ray, out float distance);
            mouseWorldPosition.Value = ray.GetPoint(distance);
        }
        #endregion

        public Vector2 GetMoving() =>
            inputActions.FPS.Mooving.ReadValue<Vector2>();

        public void SetInputActive(bool isActive) {
            if (isActive) inputActions.Enable();
            else inputActions.Disable();
        }


        private Vector2 GetMousePosition() =>
            inputActions.FPS.MousePosition.ReadValue<Vector2>();
        private void JumpPerformed(InputAction.CallbackContext obj) =>
            OnJumpEvent?.Invoke();
        private void ShootPerformed(InputAction.CallbackContext obj) =>
            OnShootingStartedEvent?.Invoke();

        private void ShootEndPerformed(InputAction.CallbackContext obj) =>
            OnShootingEndedEvent?.Invoke();


    }
}
