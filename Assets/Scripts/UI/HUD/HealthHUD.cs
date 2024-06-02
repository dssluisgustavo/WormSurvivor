using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Worm;

namespace UI.HUD
{
    public class HealthHUD : MonoBehaviour
    {
        public Image healthPrefab;
    
        private List<Image> _healthImgList = new();
    
        public void Initialize(Health health)
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        
            for (int i = 0; i < health.InitialHealth; i++)
            {
                var healthImg = Instantiate(healthPrefab, transform);
                _healthImgList.Add(healthImg);
            }
        
            UpdateHealth(_healthImgList.Count);
        
            health.OnHealthChanged += UpdateHealth;
        }

        private void UpdateHealth(int health)
        {
            for (int i = 0; i < _healthImgList.Count; i++)
            {
                _healthImgList[i].enabled = i < health;
            }
        }
    }
}
