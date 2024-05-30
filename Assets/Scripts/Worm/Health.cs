using UnityEngine;

namespace Worm
{
    public class Health : MonoBehaviour
    {
        public int InitialHealth = 3;
        private int CurrentHealth;
    
        private void Start()
        {
            CurrentHealth = InitialHealth;
        }

        public void Damage()
        {
            CurrentHealth--;
        }

        public void Recover()
        {
            CurrentHealth++;
        }
    }
}