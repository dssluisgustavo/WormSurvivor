using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private RectTransform panel;
        [SerializeField] private TextMeshProUGUI title;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void PlayAgain()
        {
            SceneController.LoadGame();
        }

        public void Exit()
        {
            SceneController.LoadMenu();
        }

        public void ShowWindow(string winnerName)
        {
            panel.localScale = Vector3.zero;
            background.DOFade(0f, 0f);
            title.text = winnerName + " Wins!";
            _canvas.enabled = true;

            DOTween.Sequence()
                .AppendInterval(2f)
                .Append(background.DOFade(.9f, .25f))
                .Append(panel.DOScale(Vector3.one, .5f).SetEase(Ease.OutElastic));
        }
    }
}