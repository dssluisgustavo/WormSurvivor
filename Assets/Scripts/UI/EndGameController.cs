using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EndGameController : MonoBehaviour
    {
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
            title.text = winnerName + " Wins!";
            _canvas.enabled = true;
        }
    }
}