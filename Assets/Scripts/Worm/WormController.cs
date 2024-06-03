using System;
using Audio;
using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class WormController : MonoBehaviour
    {
        [field: SerializeField] public string WormName { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Stamina Stamina { get; private set; }

        [field: SerializeField] public bool IsSafe { get; private set; }

        private Vector3 _initialPosition;
        private Vector3 _safePosition = new Vector3(0f, -0.75f, 0f);
        [SerializeField] private float hideDuration = 0.1f;
        [SerializeField] private float unHideDuration = 0.3f;
        private Tween _currentTween;
        private AudioManager _audioManager;
        public bool IsDead => Health.IsDead;
        public bool IsCPU { get; set; }
        public int SpotIndex { get; set; }

        private void Start()
        {
            _initialPosition = transform.localPosition;
            
            if(_audioManager)
                Health.OnDamaged += _audioManager.PlayWormHit;
        }

        private void OnEnable()
        {
            Stamina.OnStaminaDepleted += Unhide;
            Health.OnDeath += DieAnimation;
        }

        private void OnDisable()
        {
            Stamina.OnStaminaDepleted -= Unhide;
            Health.OnDeath -= DieAnimation;
            if (_audioManager)
                Health.OnDamaged -= _audioManager.PlayWormHit;
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
                .DOLocalMove(_initialPosition, unHideDuration)
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
                .AppendCallback(() =>
                {
                    if (_audioManager) 
                        _audioManager.PlayWormDeath();
                })
                .Append(transform.DOLocalMove(_safePosition - new Vector3(0f, 5f), 1f));
        }

        public void SetWormName(string player)
        {
            WormName = player;
        }
        
        public void SetSpotIndex(int index)
        {
            SpotIndex = index;
        }

        public void SetAudioManager(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }
    }
}