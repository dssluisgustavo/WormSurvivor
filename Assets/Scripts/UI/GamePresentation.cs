using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePresentation : MonoBehaviour
    {
        [SerializeField] private Image panel;
        [SerializeField] private RectTransform textRect;

        private Tween _currentTween;
    
        public Tween ShowPresentation()
        {
            if(_currentTween == null)
                _currentTween.Kill();
        
            _currentTween = DOTween.Sequence()
                .Append(textRect.DOScale(Vector3.zero, 0f))
                .Append(panel.DOFade(.9f, .25f))
                .Append(textRect.DOScale(Vector3.one, .5f))
                .Append(GetTextAnimationTween())
                .Append(textRect.DOScale(Vector3.zero, .5f))
                .Append(panel.DOFade(0f, .25f));

            return _currentTween;
        }

        private Tween GetTextAnimationTween()
        {
            return DOTween.Sequence()
                .Append(textRect.DOScale(new Vector3(.95f, .95f), .25f))
                .Append(textRect.DOScale(Vector3.one, .25f))
                .SetLoops(5);
        }
    }
}