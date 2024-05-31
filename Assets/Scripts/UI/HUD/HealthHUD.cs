using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Worm;

public class HealthHUD : MonoBehaviour
{
    public Image healthPrefab;
    
    private List<Image> _healthImgList = new();
    
    public void Initialize(Health health)
    {
        for (int i = 0; i < health.InitialHealth; i++)
        {
            var healthImg = Instantiate(healthPrefab, transform);
            _healthImgList.Add(healthImg);
        }

        foreach (Transform child in transform)
            Destroy(child.gameObject);
        
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
