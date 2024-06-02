using System;
using Managers;
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
            SceneController.LoadGame();
        }

        public void Exit()
        {
            SceneController.LoadMenu();
        }

        public void ShowWindow(string winnerName)
        {
            title.text = winnerName + " Wins!";
            canvas.enabled = true;
        }
    }
}