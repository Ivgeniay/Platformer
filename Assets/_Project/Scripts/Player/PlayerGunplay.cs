using Gun;
using PlayerInput;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerGunplay : MonoBehaviour
    {
        [SerializeField] private Gun.Gun gun;
        private Rigidbody rigidBody;
        private bool isTryingToShoot = false;

        private void Awake() {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update() {
            if (!isTryingToShoot) return;
            Shoot();
        }

        private void OnEnable() {
            Subscribe();
        }
        private void OnDisable() {
            Unsubscribe();
        }

        private void Subscribe() {
            InputSys.Instance.OnShootingStartedEvent += OnShootingStartedHandler;
            InputSys.Instance.OnShootingEndedEvent += OnShootingEndedHandler;
        }
        private void Unsubscribe() {
            InputSys.Instance.OnShootingStartedEvent -= OnShootingStartedHandler;
            InputSys.Instance.OnShootingEndedEvent -= OnShootingEndedHandler;
        }
        private void GetRecoil(Rigidbody rigidbody, Gun.Gun gun) {
            rigidbody.AddForce(-gun.transform.forward * gun.Recoil, ForceMode.Impulse);
        }

        private void OnShootingStartedHandler() =>
            isTryingToShoot = true;

        private void OnShootingEndedHandler() =>
            isTryingToShoot = false;

        private void Shoot()
        {
            var bullet = gun.Shoot();
            if (bullet != null)
                GetRecoil(rigidBody, gun);
        }
    }
}
