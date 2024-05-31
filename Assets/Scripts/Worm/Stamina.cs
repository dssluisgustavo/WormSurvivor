using System;
using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class Stamina : MonoBehaviour
    {
        public float maxStamina;
        public float depleteDuration;
        public float recoverDuration;
        
        private float currentStamina;
        public event Action<float> OnStaminaChanged = _ => { };
        public event Action OnStaminaDepleted = () => { };
        private Tween currentTween;
        
        private void Start()
        {
            currentStamina = maxStamina;
        }

        public void UseStamina()
        {
            var interval = currentStamina / 100f;
            
            if (currentTween != null)
            {
                currentTween.Kill();
                currentTween = null;
            }
            
            currentTween = DOTween
                .To(
                    () => currentStamina, 
                    v =>
                    {
                        currentStamina = v;
                        OnStaminaChanged.Invoke(currentStamina);
                    }, 
                    0f, 
                    depleteDuration * interval)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    currentTween = null;
                    OnStaminaDepleted();
                });
        }

        public void RecoverStamina()
        {
            var interval = (100f - currentStamina) / 100f;
            
            if (currentTween != null)
            {
                currentTween.Kill();
                currentTween = null;
            }
            
            currentTween = DOTween
                .To(
                    () => currentStamina, 
                    v =>
                    {
                        currentStamina = v;
                        OnStaminaChanged.Invoke(currentStamina);
                    }, 
                    100f, 
                    recoverDuration * interval)
                .SetEase(Ease.Linear)
                .OnComplete(() => currentTween = null);
        }
    }
}
