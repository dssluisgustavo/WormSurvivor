using System;
using UnityEngine;

namespace Worm
{
    public class Health : MonoBehaviour
    {
        public int InitialHealth = 3;
        private int CurrentHealth;
        
        public event Action<int> OnHealthChanged = delegate { };
    
        private void Start()
        {
            CurrentHealth = InitialHealth;
        }

        public void Damage()
        {
            CurrentHealth--;
            OnHealthChanged.Invoke(CurrentHealth);
        }

        public void Recover()
        {
            CurrentHealth++;
            OnHealthChanged.Invoke(CurrentHealth);
        }
    }
}