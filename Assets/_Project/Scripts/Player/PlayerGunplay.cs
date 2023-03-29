using Gun;
using PlayerInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerGunplay : MonoBehaviour
    {
        public event Action<Gun.Gun> OnGunChangedEvent;

        [SerializeField] private List<Gun.Gun> gunList = new List<Gun.Gun>();
        [SerializeField] private bool isUnscaleFire = true;

        public Gun.Gun CurrentGun { get; private set; }
        private Rigidbody rigidBody;
        private bool isTryingToShoot = false;

        private void Awake() {
            gunList.ForEach(gun => {
                gun.SetUp(isUnscaleFire);
            });

            rigidBody = GetComponent<Rigidbody>();
            CurrentGun = ChangeGun(0, gunList);
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


        public List<Gun.Gun> GetGuns() {
            var gunList2 = new List<Gun.Gun>();
            gunList.ForEach(el => gunList2.Add(el));
            return gunList2;
        }

        private void Subscribe() {
            InputSys.Instance.OnShootingStartedEvent += OnShootingStartedHandler;
            InputSys.Instance.OnShootingEndedEvent += OnShootingEndedHandler;
            InputSys.Instance.OnChangeGunEvent += OnChangeGunHandler;
        }


        private void Unsubscribe() {
            InputSys.Instance.OnShootingStartedEvent -= OnShootingStartedHandler;
            InputSys.Instance.OnShootingEndedEvent -= OnShootingEndedHandler;
            InputSys.Instance.OnChangeGunEvent -= OnChangeGunHandler;
        }
        private void GetRecoil(Rigidbody rigidbody, Gun.Gun gun) {
            rigidbody.AddForce(-gun.transform.forward * gun.Recoil, ForceMode.Impulse);
        }

        private void OnShootingStartedHandler() =>
            isTryingToShoot = true;

        private void OnShootingEndedHandler() =>
            isTryingToShoot = false;

        private void OnChangeGunHandler(int obj) =>
            CurrentGun = ChangeGun(obj-1, gunList);

        private void Shoot()
        {
            var bullet = CurrentGun.Shoot();
            if (bullet != null) {
                GetRecoil(rigidBody, CurrentGun);
                if (bullet.isLast) {
                    CurrentGun = ChangeGun(0, gunList);
                }
            }
            
        }


        private Gun.Gun ChangeGun(int index, List<Gun.Gun> gunList)
        {
             gunList.ForEach(el => el.gameObject.SetActive(false));

            if (index >= gunList.Count || index < 0) {
                gunList[0].gameObject.SetActive(true);
                OnGunChangedEvent?.Invoke(gunList[0]);
                return gunList[0];
            }

            gunList[index].gameObject.SetActive(true);

            OnGunChangedEvent?.Invoke(gunList[index]);
            return gunList[index];
        }
    }
}
