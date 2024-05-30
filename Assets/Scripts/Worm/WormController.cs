using DG.Tweening;
using UnityEngine;

namespace Worm
{
    public class WormController : MonoBehaviour
    {
        private bool _isSafe;

        private Vector3 safePosition = new Vector3(0f, -0.375f, 0f);
        public float hideDuration = 0.1f;
        public float unHideDuration = 0.3f;
        private Tween currentTween;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isSafe = true;

                if (currentTween != null)
                {
                    currentTween.Kill();
                    currentTween = null;
                }

                currentTween = transform
                    .DOMove(safePosition, hideDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => currentTween = null);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _isSafe = false;
                if (currentTween != null)
                {
                    currentTween.Kill();
                    currentTween = null;
                }

                currentTween = transform
                    .DOMove(Vector3.zero, unHideDuration)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => currentTween = null);
                ;
            }
        }
    }
}