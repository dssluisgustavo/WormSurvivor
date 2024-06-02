using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class WormController : MonoBehaviour
    {
        [field: SerializeField] public string WormName { get; private set; }
        [field:SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Stamina Stamina { get; private set; }
        
        [field: SerializeField] public bool IsSafe { get; private set; }

        private Vector3 _safePosition = new Vector3(0f, -0.375f, 0f);
        [SerializeField] private float hideDuration = 0.1f;
        [SerializeField] private float unHideDuration = 0.3f;
        private Tween _currentTween;
        public bool IsDead => Health.IsDead;
        public bool IsCPU { get; set; }

        private void OnEnable()
        {
            Stamina.OnStaminaDepleted += Unhide;
            Health.OnDeath += DieAnimation;
        }

        private void OnDisable()
        {
            Stamina.OnStaminaDepleted -= Unhide;
            Health.OnDeath -= DieAnimation;
        }

        public void Hide()
        {
            
            Stamina.UseStamina();
                
           TryClearTween();

            _currentTween = transform
                .DOLocalMove(_safePosition, hideDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    IsSafe = true;
                    _currentTween = null;
                });
        }

        public void Unhide()
        {
            IsSafe = false;
                
            Stamina.RecoverStamina();
                
            TryClearTween();

            _currentTween = transform
                .DOLocalMove(Vector3.zero, unHideDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => _currentTween = null);
        }
        
        void TryClearTween()
        {
            if (_currentTween != null)
            {
                _currentTween.Kill();
                _currentTween = null;
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