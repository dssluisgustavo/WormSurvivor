using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndGameController : MonoBehaviour
    {
        public TextMeshProUGUI title;
        void PlayAgain()
        {
            SceneManager.LoadScene("Game");
        }

        void Quit()
        {
            SceneManager.LoadScene("Menu");
        }

        public void SetAsVictory()
        {
            ChangeTitle("Congratulations!!!\nYou Win");
        }

        public void SetAsDefeat()
        {
            ChangeTitle("You Lose!!!");
        }
        private void ChangeTitle(string message)
        {
            title.text = message;
        }
    }
}