using System.Collections.Generic;
using System.Linq;
using Eagle;
using UnityEngine;
using UnityEngine.Serialization;
using Worm;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EagleSpawner : MonoBehaviour
    {
        public EagleController eagleControllerPrefab;
        
        public float maxSpawnTime;
        public float minSpawnTime;
        public float spawnTimeDecrease = 0.25f;
        
        public float eagleSpeedModIncreasedByLoop = .25f;
        
        [Header("References")]
        public GameManager gameManager;
        
        private float spawnTime;
        private float currentSpawnTime;
        private float currentEagleSpeedMod;

        private List<Transform> targets = new();

        private void Start()
        {
            spawnTime = maxSpawnTime;
            currentSpawnTime = spawnTime;
            currentEagleSpeedMod = -eagleSpeedModIncreasedByLoop; //will be setted to zero on first spawn
        }

        private void Update()
        {
            currentSpawnTime -= Time.deltaTime;
            if (currentSpawnTime <= 0)
            {
                SpawnEagle();
                spawnTime = Mathf.Clamp(spawnTime - spawnTimeDecrease, minSpawnTime, maxSpawnTime);
                currentSpawnTime = spawnTime;
                Debug.Log(Time.time);
            }
        }
        
        private void SpawnEagle()
        {
            var target = ChooseTarget();
            
            var position = Random.Range(-10f, 10f);
            var eagle = Instantiate(eagleControllerPrefab, new Vector3(position, 6f, 0), Quaternion.identity);
            eagle.Setup(5f, 1f + 1f * currentEagleSpeedMod);
            eagle.SetTarget(target.transform);
            eagle.MoveToTarget();
        }
        
        private Transform ChooseTarget()
        {
            if (targets.Count == 0)
            {
                targets = gameManager.wormSpawner.Worms.Select(_ => _.transform).ToList();
                eagleSpeedModIncreasedByLoop += 0.25f;
            }

            var target = targets[Random.Range(0, targets.Count)];
            targets.Remove(target);
            return target;
        }
    }
}