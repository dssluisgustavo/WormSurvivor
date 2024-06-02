using System;
using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class WormController : MonoBehaviour
    {
        public string wormName;
        public Health health;
        public Stamina stamina;
        
        public bool _isSafe;

        private Vector3 safePosition = new Vector3(0f, -0.375f, 0f);
        public float hideDuration = 0.1f;
        public float unHideDuration = 0.3f;
        private Tween currentTween;
        public bool IsDead => health.IsDead;

        private void OnEnable()
        {
            stamina.OnStaminaDepleted += Unhide;
            health.OnDeath += DieAnimation;
        }

        private void OnDisable()
        {
            stamina.OnStaminaDepleted -= Unhide;
            health.OnDeath -= DieAnimation;
        }

        public void Hide()
        {
            _isSafe = true;
            stamina.UseStamina();
                
           TryClearTween();

            currentTween = transform
                .DOLocalMove(safePosition, hideDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => currentTween = null);
        }

        public void Unhide()
        {
            _isSafe = false;
                
            stamina.RecoverStamina();
                
            TryClearTween();

            currentTween = transform
                .DOLocalMove(Vector3.zero, unHideDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => currentTween = null);
        }
        
        void TryClearTween()
        {
            if (currentTween != null)
            {
                currentTween.Kill();
                currentTween = null;
            }
        }
        
        private void DieAnimation()
        {
            transform.localPosition = Vector3.zero;
            DOTween.Sequence()
                .Append(transform.DOShakePosition(1f))
                .Append(transform.DOLocalMove(Vector3.zero, .25f))
                .Append(transform.DOLocalMove(new Vector2(0f, -0.75f), 1f));
        }
    }
}