using Gun;
using PlayerInput;
using UnityEngine;

namespace Player
{
    public class PlayerGunplay : MonoBehaviour
    {
        [SerializeField] private AmmoGun gun;
        private Rigidbody rigidBody;

        private void Awake() {
            rigidBody = GetComponent<Rigidbody>();
        }
        private void OnEnable() {
            Subscribe();
        }
        private void OnDisable() {
            Unsubscribe();
        }

        private void Subscribe() {
            InputSys.Instance.OnShootEvent += OnShootHandler;
        }
        private void Unsubscribe() {
            InputSys.Instance.OnJumpEvent -= OnShootHandler;
        }

        private void OnShootHandler() {
            var bullet = gun.Shoot();
            if (bullet != null)
                GetRecoil(rigidBody, gun);
        }
        private void GetRecoil(Rigidbody rigidbody, AmmoGun gun) {
            rigidbody.AddForce(-gun.transform.forward * gun.Recoil, ForceMode.Impulse);
        }
    }
}
