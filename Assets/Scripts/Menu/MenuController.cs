using Managers;
using UnityEditor;
using UnityEngine;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        public void Play()
        {
            SceneController.LoadGame();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        }
    }
}