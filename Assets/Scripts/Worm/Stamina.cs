using System;
using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class Stamina : MonoBehaviour
    {
        [field: SerializeField] public float MaxStamina { get; private set; }
        [SerializeField] private float depleteDuration;
        [SerializeField] private float recoverDuration;
        
        private float _currentStamina;
        public event Action<float> OnStaminaChanged = _ => { };
        public event Action OnStaminaDepleted = () => { };
        private Tween _currentTween;
        
        private void Start()
        {
            _currentStamina = MaxStamina;
        }

        public void UseStamina()
        {
            var interval = _currentStamina / 100f;
            
            if (_currentTween != null)
            {
                _currentTween.Kill();
                _currentTween = null;
            }
            
            _currentTween = DOTween
                .To(
                    () => _currentStamina, 
                    v =>
                    {
                        _currentStamina = v;
                        OnStaminaChanged.Invoke(_currentStamina);
                    }, 
                    0f, 
                    depleteDuration * interval)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _currentTween = null;
                    OnStaminaDepleted();
                });
        }

        public void RecoverStamina()
        {
            var interval = (100f - _currentStamina) / 100f;
            
            if (_currentTween != null)
            {
                _currentTween.Kill();
                _currentTween = null;
            }
            
            _currentTween = DOTween
                .To(
                    () => _currentStamina, 
                    v =>
                    {
                        _currentStamina = v;
                        OnStaminaChanged.Invoke(_currentStamina);
                    }, 
                    100f, 
                    recoverDuration * interval)
                .SetEase(Ease.Linear)
                .OnComplete(() => _currentTween = null);
        }
    }
}
