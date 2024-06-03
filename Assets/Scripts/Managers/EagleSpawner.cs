using System;
using System.Collections.Generic;
using System.Linq;
using Eagle;
using UnityEngine;
using Worm;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EagleSpawner : MonoBehaviour
    {
        [SerializeField] private EagleController eagleControllerPrefab;

        [SerializeField] private float maxSpawnTime;
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float spawnTimeDecrease = 0.25f;

        [SerializeField] private float eagleSpeedModIncreasedByLoop = .25f;

        [Header("References")] [SerializeField] private GameManager gameManager;

        private float _spawnTime;
        private float _currentSpawnTime;
        private float _currentEagleSpeedMod;

        private List<WormController> _targets = new();

        private void Awake()
        {
            gameManager.OnGameStart += StartSpawner;
            gameManager.OnGameEnd += StopSpawner;
        }

        private void Start()
        {
            _spawnTime = maxSpawnTime;
            _currentSpawnTime = Mathf.Infinity;
            _currentEagleSpeedMod = -eagleSpeedModIncreasedByLoop; //will be setted to zero on first spawn
        }

        private void OnDisable()
        {
            gameManager.OnGameStart -= StartSpawner;
            gameManager.OnGameEnd -= StopSpawner;
        }

        private void Update()
        {
            _currentSpawnTime -= Time.deltaTime;
            if (_currentSpawnTime <= 0)
            {
                SpawnEagle();
                _spawnTime = Mathf.Clamp(_spawnTime - spawnTimeDecrease, minSpawnTime, maxSpawnTime);
                _currentSpawnTime = _spawnTime;
            }
        }

        private void SpawnEagle()
        {
            var target = ChooseTarget();
            if (!target) return;

            var position = Random.Range(-10f, 10f);
            var eagle = Instantiate(eagleControllerPrefab, new Vector3(position, 6f, 0), Quaternion.identity);
            eagle.SetGameManager(gameManager);
            eagle.Setup(5f, 1f + 1f * _currentEagleSpeedMod);
            eagle.SetTarget(target.transform);
            eagle.MoveToTarget();
        }

        private Transform ChooseTarget()
        {
            if (_targets.Count == 0)
            {
                _targets = gameManager.WormSpawner.Worms.ToList();
                _currentEagleSpeedMod += 0.25f;
            }

            Transform target = null;
            do
            {
                if (_targets.Count == 0) break;

                var worm = _targets[Random.Range(0, _targets.Count)];
                if (!worm.IsDead)
                    target = worm.transform;
                _targets.Remove(worm);
            } while (!target);

            return target;
        }

        private void StartSpawner()
        {
            _currentSpawnTime = _spawnTime;
        }

        private void StopSpawner()
        {
            gameObject.SetActive(false);
        }
    }
}