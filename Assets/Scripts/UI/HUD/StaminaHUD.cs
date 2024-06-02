using UnityEngine;
using UnityEngine.UI;
using Worm;

namespace UI.HUD
{
    public class StaminaHUD : MonoBehaviour
    {
        public Slider slider;
    
        public void Initialize(Stamina stamina)
        {
            slider.maxValue = stamina.maxStamina;
        
            UpdateStamina(slider.maxValue);
        
            stamina.OnStaminaChanged += UpdateStamina;
        }

        private void UpdateStamina(float value)
        {
            slider.value = value;
        }
    }
}
