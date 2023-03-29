using System;
using System.Collections;
using UnityEngine;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        public event Action OnShootEvent;
        public event Action<int> OnAmmoChangetEvent;
        [field: SerializeField] public float Recoil { get; private set; }
        [field: SerializeField] public int Ammo { get; private set; }
        [field: SerializeField] public bool IsInfinityAmmo { get; private set; }

        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private int gunDamage = 1;
        [SerializeField] private float spawnPeriod = 0.2f;
        [SerializeField] private float flashPeriod = 0.05f;
        [SerializeField] private AudioSource shootSound;
        [SerializeField] private GameObject shootFlash;

        private bool isUnscaleFire;

        private bool canShoot = true;
        private Coroutine gunTimeoutCoroutine = null;
        private Coroutine flashTimeoutCoroutine = null;

        private void OnDisable() {
            if (gunTimeoutCoroutine is not null) {
                StopCoroutine(gunTimeoutCoroutine);
                canShoot = true;
            }
            if (flashTimeoutCoroutine is not null) {
                StopCoroutine(flashTimeoutCoroutine);
                shootFlash.SetActive(false);
            }
        }

        public Bullet Shoot()
        {
            if (!canShoot) return null;
            if (Ammo > 0 || IsInfinityAmmo) {
                var bullet = Instantiate(bulletPrefab, spawnTransform.position, spawnTransform.rotation, null);
                bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
                gunTimeoutCoroutine = StartCoroutine(ShootTimerRoutine(spawnPeriod));
                flashTimeoutCoroutine = StartCoroutine(OffFlashRoutine(flashPeriod));
                shootSound.Play();
                shootFlash.SetActive(true);
                if (!IsInfinityAmmo) Ammo -= 1;
                bullet.SetUp(gunDamage, Ammo == 0 && !IsInfinityAmmo);
                OnShootEvent?.Invoke();
                OnAmmoChangetEvent?.Invoke(Ammo);
                return bullet;
            }

            return null;
        }

        private IEnumerator ShootTimerRoutine(float delay) { 
            canShoot = false;

            if (isUnscaleFire) yield return new WaitForSecondsRealtime(delay);
            else yield return new WaitForSeconds(delay);

            canShoot = true;
        }

        private IEnumerator OffFlashRoutine(float delay) {
            if (isUnscaleFire) yield return new WaitForSecondsRealtime(delay);
            else yield return new WaitForSeconds(delay);
            shootFlash.SetActive(false);
        }

        public void AddBullets(int amount) {
            Ammo += amount;
            OnAmmoChangetEvent?.Invoke(Ammo);
        }  
        
        public void SetUp(bool isUnscaleFire) {
            this.isUnscaleFire = isUnscaleFire;
        }
        
    }
}
