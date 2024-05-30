using System;
using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class Stamina : MonoBehaviour
    {
        public float initialStamina;
        public float currentStamina;
        public float duration;
        public event Action OnStaminaDepleted = () => { };
        private Tween currentTween;

        private void Start()
        {
            currentStamina = initialStamina;
        }

        public void UseStamina()
        {
            var interval = currentStamina / 100f;
            
            DOTween
                .To(() => currentStamina, v => currentStamina = v, 0f, duration * interval)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    currentTween = null;
                    OnStaminaDepleted();
                });
            
            if (currentTween != null)
            {
                currentTween.Kill();
                currentTween = null;
            }
            
        }

        public void RecoverStamina()
        {
            var interval = (100f - currentStamina) / 100f;
            
            DOTween
                .To(() => currentStamina, v => currentStamina = v, 100f, duration * interval)
                .SetEase(Ease.Linear)
                .OnComplete(() => currentTween = null);
            
            if (currentTween != null)
            {
                currentTween.Kill();
                currentTween = null;
            }
        }
    }
}
