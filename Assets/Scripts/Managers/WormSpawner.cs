using System.Collections.Generic;
using UI.HUD;
using Unity.VisualScripting;
using UnityEngine;
using Worm;

namespace Managers
{
    public class WormSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnSpots;
        [SerializeField] private RectTransform characterHUDParent;
    
        [SerializeField] private GameObject characterPrefab;
        [SerializeField] private CharacterHUD characterHUDPrefab;

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

            WormInput wormInput;
            if (isCPU)
            {
                var cpuObj = new GameObject("CPU_AI");
                cpuObj.transform.SetParent(worm.transform);
                cpuObj.transform.localPosition = Vector3.zero;
                
                worm.SetWormName($"CPU - {worms.Count}");
                
                wormInput = cpuObj.AddComponent<WormAI>();
                ((WormAI)wormInput).Setup(2f);
            }
            else
            {
                worm.SetWormName("Player");
                wormInput = worm.AddComponent<PlayerInput>();
            }

            characterHUD.Initialize(worm);
            wormInput.Setup(worm);
        
            worms.Add(worm);
            return worm;
        }
    }
}
