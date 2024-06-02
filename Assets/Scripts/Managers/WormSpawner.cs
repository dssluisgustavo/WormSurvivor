using System.Collections.Generic;
using UI.HUD;
using Unity.VisualScripting;
using UnityEngine;
using Worm;

namespace Managers
{
    public class WormSpawner : MonoBehaviour
    {
        public Transform[] spawnSpots;
        public RectTransform characterHUDParent;
    
        public GameObject characterPrefab;
        public CharacterHUD characterHUDPrefab;

        private List<WormController> worms = new();
        public WormController[] Worms => worms.ToArray();
    
        public WormController SpawnWorm(bool isCPU)
        {
            if (spawnSpots.Length == worms.Count)
            {
                Debug.LogError("No more spawn spots available");
                return null;
            }
        
            var spot = spawnSpots[worms.Count];
            var character = Instantiate(characterPrefab, spot.position, Quaternion.identity);
            var characterHUD = Instantiate(characterHUDPrefab, characterHUDParent);

            var worm = character.GetComponentInChildren<WormController>();
            worm.IsCPU = isCPU;
            characterHUD.Initialize(worm);

            WormInput wormInput;
            if (isCPU)
            {
                var cpuObj = Instantiate(new GameObject("CPU_AI"), worm.transform, true);
                cpuObj.transform.localPosition = Vector3.zero;
                
                wormInput = cpuObj.AddComponent<WormAI>();
                ((WormAI)wormInput).Setup(2f);
            }
            else
            {
                wormInput = worm.AddComponent<PlayerInput>();
            }

            wormInput.Setup(worm);
        
            worms.Add(worm);
            return worm;
        }
    }
}
