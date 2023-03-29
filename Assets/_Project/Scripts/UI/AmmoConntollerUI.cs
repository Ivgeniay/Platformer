using Gun;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    internal class AmmoConntollerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammoTMP;
        [SerializeField] private string template;
        private PlayerGunplay playerGunplay;
        private Gun.Gun gun;

        private void Awake()
        {
            playerGunplay = FindObjectOfType<PlayerGunplay>();
            ammoTMP.gameObject.SetActive(false);
        }

        private void OnEnable() {
            playerGunplay.OnGunChangedEvent += OnGunChengedHandler;
        }

        private void OnDisable() {
            playerGunplay.OnGunChangedEvent -= OnGunChengedHandler;
        }

        private void OnGunChengedHandler(Gun.Gun obj)
        {
            if (gun is not null) gun.OnAmmoChangetEvent -= OnAmmoChangetHandler;

            gun = obj;
            gun.OnAmmoChangetEvent += OnAmmoChangetHandler;

            if (!gun.IsInfinityAmmo) {
                ammoTMP.gameObject.SetActive(true);
                ammoTMP.text = template + gun.Ammo.ToString();
            }
            else {
                ammoTMP.gameObject.SetActive(false);
            }
        }

        private void OnAmmoChangetHandler(int obj)
        {
            ammoTMP.text = template + gun.Ammo.ToString();
        }

    }
}
