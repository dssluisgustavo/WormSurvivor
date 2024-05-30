using System;
using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class WormController : MonoBehaviour
    {
        public Stamina stamina;
        
        private bool _isSafe;

        private Vector3 safePosition = new Vector3(0f, -0.375f, 0f);
        public float hideDuration = 0.1f;
        public float unHideDuration = 0.3f;
        private Tween currentTween;


        private void OnEnable()
        {
            stamina.OnStaminaDepleted += Unhide;
        }

        private void OnDisable()
        {
            stamina.OnStaminaDepleted -= Unhide;
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Hide();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Unhide();
            }
        }

        void Hide()
        {
            _isSafe = true;
            stamina.UseStamina();
                
           TryClearTween();

            currentTween = transform
                .DOMove(safePosition, hideDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => currentTween = null);
        }

        void Unhide()
        {
            _isSafe = false;
                
            stamina.RecoverStamina();
                
            TryClearTween();

            currentTween = transform
                .DOMove(Vector3.zero, unHideDuration)
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
    }
}