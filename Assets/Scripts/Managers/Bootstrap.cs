using UnityEngine;

namespace Managers
{
    public class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void CreatePersistentObject()
        {
            var persistentObjectPrefab = Resources.Load("PersistentCanvas");
            var persistentObject = Object.Instantiate(persistentObjectPrefab);
            Object.DontDestroyOnLoad(persistentObject);
        }
    }
}