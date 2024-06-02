using System;
using UnityEngine;

namespace Worm
{
    public class Health : MonoBehaviour
    {
        public int InitialHealth = 3;
        private int CurrentHealth;

        public GameObject overrideObjectToDestroy;
        
        public event Action<int> OnHealthChanged = delegate { };
        public event Action OnDeath = delegate { };
        
        public bool IsDead { get; private set; }
    
        private void Start()
        {
            CurrentHealth = InitialHealth;
            if (!overrideObjectToDestroy)
                overrideObjectToDestroy = gameObject;
        }

        public void Damage()
        {
            if (CurrentHealth == 0) return;
            
            CurrentHealth--;

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                OnDeath();
            }

            OnHealthChanged.Invoke(CurrentHealth);
        }

        public void Recover()
        {
            CurrentHealth++;
            OnHealthChanged.Invoke(CurrentHealth);
        }
    }
}