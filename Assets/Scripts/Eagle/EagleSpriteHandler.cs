using UnityEngine;

namespace Eagle
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EagleSpriteHandler : MonoBehaviour
    {
        [SerializeField] private Sprite risingSprite;
    
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    
        public void ChangeToRisingSprite()
        {
            _spriteRenderer.sprite = risingSprite;
        }

        public void FixRotation(float xDirection)
        {
            transform.rotation = Quaternion.identity;

            var localScaleDirection = -1f;
            if (_spriteRenderer.sprite != risingSprite && xDirection < 0
                || _spriteRenderer.sprite == risingSprite && xDirection > 0)
                localScaleDirection = 1f;
        
            transform.localScale = new Vector3(localScaleDirection, 1, 1);
        }
    }
}
