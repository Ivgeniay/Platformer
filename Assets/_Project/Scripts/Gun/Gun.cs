using System.Collections;
using UnityEngine;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        [field: SerializeField] public float Recoil { get; private set; }

        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private int gunDamage = 1;
        [SerializeField] private float spawnPeriod = 0.2f;
        [SerializeField] private float flashPeriod = 0.05f;
        [SerializeField] private AudioSource shootSound;
        [SerializeField] private GameObject shootFlash;

        private bool canShoot = true;
        private Coroutine gunTimeoutCoroutine = null;
        private Coroutine flashTimeoutCoroutine = null;

        public Bullet Shoot()
        {
            if (!canShoot) return null;
            var bullet = Instantiate(bulletPrefab, spawnTransform.position, spawnTransform.rotation, null);
            bullet.SetUp(gunDamage);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            gunTimeoutCoroutine = StartCoroutine(ShootTimerRoutine(spawnPeriod));
            flashTimeoutCoroutine = StartCoroutine(OffFlashRoutine(flashPeriod));
            shootSound.Play();
            shootFlash.SetActive(true);
            return bullet;
        }

        private IEnumerator ShootTimerRoutine(float delay) { 
            canShoot = false;
            yield return new WaitForSeconds(delay);
            canShoot = true;
        }

        private IEnumerator OffFlashRoutine(float delay) {
            yield return new WaitForSeconds(delay);
            shootFlash.SetActive(false);
        }
    }
}
