using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    internal class HealthUI : MonoBehaviour
    {
        [SerializeField] private GameObject healthIcon;
        private List<GameObject> healthList = new();

        public void SetUp(int maxHealth)
        {
            for (int i = 0; i < maxHealth; i++) {
                var newIcon = Instantiate(healthIcon, transform);
                healthList.Add(newIcon);
            }
        }

        public void DisplayHealth(int health)
        {
            int count = 0;
            foreach (var icon in healthList) {
                if (count < health)
                    icon.SetActive(true);
                else
                    icon.SetActive(false);

                count++;
            }
        }
    }
}
