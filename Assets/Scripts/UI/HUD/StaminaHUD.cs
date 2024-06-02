using UnityEngine;
using UnityEngine.UI;
using Worm;

namespace UI.HUD
{
    public class StaminaHUD : MonoBehaviour
    {
        [SerializeField] private Slider slider;
    
        public void Initialize(Stamina stamina)
        {
            slider.maxValue = stamina.MaxStamina;
        
            UpdateStamina(slider.maxValue);
        
            stamina.OnStaminaChanged += UpdateStamina;
        }

        private void UpdateStamina(float value)
        {
            slider.value = value;
        }
    }
}
