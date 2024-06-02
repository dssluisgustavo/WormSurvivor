using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pause
{
    public class PauseController : MonoBehaviour
    {
        public Canvas canvas;
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
            canvas.enabled = true;
        }
        
        public void Resume()
        {
            Time.timeScale = 1f;
            canvas.enabled = false;
        }

        public void BackToMainMenu()
        {
            Resume();
            SceneController.LoadMenu();
        }
    }
}
