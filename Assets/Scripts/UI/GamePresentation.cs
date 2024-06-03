using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePresentation : MonoBehaviour
    {
        [SerializeField] private Image panel;
        [SerializeField] private RectTransform textRect;
        [SerializeField] private RectTransform tutorialRect;

        private Tween _currentTween;
    
        public Tween ShowPresentation()
        {
            if(_currentTween == null)
                _currentTween.Kill();
            
            textRect.localScale = Vector3.zero;

            _currentTween = DOTween.Sequence()
                .Append(panel.DOFade(.9f, .25f))
                .Append(GetTextAnimationTween(textRect, 5))
                .Append(panel.DOFade(0f, .25f));

            return _currentTween;
        }

        public Tween ShowTutorial()
        {
            tutorialRect.localScale = Vector3.zero;
            return GetTextAnimationTween(tutorialRect, 8, .99f);
        }

        private Tween GetTextAnimationTween(RectTransform rect, int loops, float targetSize = .95f)
        {
            return DOTween.Sequence()
                .Append(rect.DOScale(Vector3.one, .5f))
                .Append(
                    DOTween.Sequence()
                        .Append(rect.DOScale(new Vector3(targetSize, targetSize), .25f))
                        .Append(rect.DOScale(Vector3.one, .25f))
                        .SetLoops(loops)
                    )
                .Append(rect.DOScale(Vector3.zero, .5f));
        }
    }
}