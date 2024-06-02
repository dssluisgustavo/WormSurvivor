using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class SceneController
    {
        private static Image _blackScreen;
        
        public static void LoadMenu()
        {
            LoadScene("Menu");
        }

        public static void LoadGame()
        {
            LoadScene("Game");
        }
        
        public static void LoadScene(string sceneName)
        {
            if(!_blackScreen)
                _blackScreen = Object.FindFirstObjectByType<BlackScreen>().Image;

            _blackScreen.DOFade(1f, 1f).OnComplete(
                () =>
                {
                    var asyncOp = SceneManager.LoadSceneAsync(sceneName);
                    asyncOp.completed += (operation) =>
                    {
                        _blackScreen.DOFade(0f, 1f);          
                    };
                }
            ).SetUpdate(true);
        }
    }
}