using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Pause
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image background;
        [SerializeField] private RectTransform panel;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Mathf.Approximately(Time.timeScale, 1f))
                {
                    Pause();
                }
                else
                {
                    Resume();
                }
            }
        }

        void Pause()
        {
            Time.timeScale = 0f;
            ShowWindow();
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            HideWindow();
        }

        public void BackToMainMenu()
        {
            Resume();
            SceneController.LoadMenu();
        }

        public void ShowWindow()
        {
            panel.localScale = Vector3.zero;
            background.DOFade(0f, 0f);
            canvas.enabled = true;
            DOTween.Sequence()
                .Append(background.DOFade(.9f, .1f))
                .Append(panel.DOScale(Vector3.one, .25f).SetEase(Ease.OutBack))
                .SetUpdate(true);
        }

        public void HideWindow()
        {
            DOTween.Sequence()
                .Append(panel.DOScale(Vector3.zero, .25f).SetEase(Ease.InBack))
                .Append(background.DOFade(0f, .1f))
                .SetUpdate(true)
                .OnComplete(() => canvas.enabled = true);
        }
    }
}