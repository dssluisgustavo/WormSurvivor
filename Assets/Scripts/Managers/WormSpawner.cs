using System.Collections.Generic;
using System.Linq;
using Audio;
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

        [SerializeField] private AudioManager audioManager;

        private List<WormController> worms = new();
        public WormController[] Worms => worms.ToArray();

        private Dictionary<Transform, bool> _spotsUsed = new();

        private void Awake()
        {
            for (int i = 0; i < spawnSpots.Length; i++)
                _spotsUsed.Add(spawnSpots[i], false);
        }

        public WormController SpawnWorm(bool isCPU)
        {
            if (spawnSpots.Length == worms.Count)
            {
                Debug.LogError("No more spawn spots available");
                return null;
            }

            var spot = GetRandomFreeSpot();
            var character = Instantiate(characterPrefab, spot.position, Quaternion.identity);
            var characterHUD = Instantiate(characterHUDPrefab, characterHUDParent);

            var worm = character.GetComponentInChildren<WormController>();
            worm.IsCPU = isCPU;
            worm.SetAudioManager(audioManager);

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
            worm.SetSpotIndex(System.Array.IndexOf(spawnSpots, spot));
            wormInput.Setup(worm);

            worms.Add(worm);
            return worm;
        }

        private Transform GetRandomFreeSpot()
        {
            Transform spot;
            do
            {
                var randomNumber = Random.Range(0, spawnSpots.Length);
                spot = spawnSpots[randomNumber];
            } while (_spotsUsed[spot]);

            _spotsUsed[spot] = true;
            return spot;
        }

        public void FixCharacterHUDOrder()
        {
            var childs = characterHUDParent.GetComponentsInChildren<CharacterHUD>().ToList();
            childs = childs.OrderBy(_ => _.WormControllerReference.SpotIndex).ToList();

            for (int i = 0; i < childs.Count; i++)
                childs[i].transform.SetSiblingIndex(i);
        }
    }
}