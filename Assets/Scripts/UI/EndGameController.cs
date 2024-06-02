using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndGameController : MonoBehaviour
    {
        public TextMeshProUGUI title;
        private Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene("Game");
        }

        public void Exit()
        {
            SceneManager.LoadScene("Menu");
        }

        public void ShowWindow(string winnerName)
        {
            title.text = winnerName + " Wins!";
            canvas.enabled = true;
        }
    }
}