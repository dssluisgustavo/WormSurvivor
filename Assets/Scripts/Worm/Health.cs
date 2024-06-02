using System;
using UnityEngine;

namespace Worm
{
    public class Health : MonoBehaviour
    {
        [field: SerializeField] public int InitialHealth { get; private set; } = 3;
        private int _currentHealth;

        [SerializeField] private GameObject overrideObjectToDestroy;
        
        public event Action<int> OnHealthChanged = delegate { };
        public event Action OnDeath = delegate { };
        
        public bool IsDead { get; private set; }
    
        private void Start()
        {
            _currentHealth = InitialHealth;
            if (!overrideObjectToDestroy)
                overrideObjectToDestroy = gameObject;
        }

        public void Damage()
        {
            if (_currentHealth == 0) return;
            
            _currentHealth--;

            if (_currentHealth <= 0)
            {
                IsDead = true;
                OnDeath();
            }

            OnHealthChanged.Invoke(_currentHealth);
        }

        public void Recover()
        {
            _currentHealth++;
            OnHealthChanged.Invoke(_currentHealth);
        }
    }
}