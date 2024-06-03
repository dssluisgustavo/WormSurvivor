using System;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace Worm
{
    public class Health : MonoBehaviour
    {
        [field: SerializeField] public int InitialHealth { get; private set; } = 3;
        private int _currentHealth;

        [SerializeField] private GameObject overrideObjectToDestroy;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public event Action<int> OnHealthChanged = delegate { };
        public event Action OnDeath = delegate { };

        public bool IsDead { get; private set; }

        private Tween _currentTween;
        
        private void Start()
        {
            _currentHealth = InitialHealth;
            if (!overrideObjectToDestroy)
                overrideObjectToDestroy = gameObject;
        }

        public void Damage()
        {
            if (_currentHealth == 0) return;

            _currentHealth--;

            if (_currentHealth <= 0)
            {
                IsDead = true;
                OnDeath();
            }


            FlashSprite();

            OnHealthChanged.Invoke(_currentHealth);
        }

        public void Recover()
        {
            _currentHealth++;
            OnHealthChanged.Invoke(_currentHealth);
        }

        private void FlashSprite()
        {
            if (!spriteRenderer) return;

            _currentTween = DOTween.Sequence()
                .Append(spriteRenderer.DOFade(0f, 0f))
                .AppendInterval(.1f)
                .Append(spriteRenderer.DOFade(1f, 0f))
                .AppendInterval(.1f)
                .SetLoops(3);
        }

        private void OnDestroy()
        {
            _currentTween.Kill();
        }
    }
}