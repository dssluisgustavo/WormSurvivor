using System.Collections.Generic;
using UI.HUD;
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
    
        public WormController SpawnWorm()
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
            characterHUD.Initialize(worm);
        
            worms.Add(worm);
            return worm;
        }
    }
}
