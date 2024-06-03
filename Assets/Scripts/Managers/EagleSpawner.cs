using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Eagle;
using UnityEngine;
using UnityEngine.Serialization;
using Worm;

namespace Managers
{
    public class EagleSpawner : MonoBehaviour
    {
        [SerializeField] private EagleController eagleControllerPrefab;

        [Header("SpawnTime")]
        [SerializeField] private float initialSpawnTime;
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float spawnTimeDecrease = 0.25f;
        [SerializeField] private float spawnTimeDecreaseDelay = 4f;
        
        [Header("Eagle Settings")]
        
        [SerializeField] private float eagleSpeedModIncreasedByLoop = .25f;
        [SerializeField] private float eagleSpeedModMax = 5f;

        [Header("References")] 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private AudioManager audioManager;

        public float _spawnTime;
        public float _currentSpawnTime;
        public float _currentEagleSpeedMod;

        private List<WormController> _targets = new();

        private bool _started;

        private void Start()
        {
            _spawnTime = initialSpawnTime;
            _currentSpawnTime = _spawnTime;
            _currentEagleSpeedMod = -eagleSpeedModIncreasedByLoop; //will be setted to zero on first spawn
        }
        
        private void OnEnable()
        {
            gameManager.OnGameStart += StartSpawner;
            gameManager.OnGameEnd += StopSpawner;
        }

        private void OnDisable()
        {
            gameManager.OnGameStart -= StartSpawner;
            gameManager.OnGameEnd -= StopSpawner;
        }

        private void Update()
        {
            if (!_started) return;
            
            _currentSpawnTime -= Time.deltaTime;
            if (_currentSpawnTime <= 0)
            {
                SpawnEagle();
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

            var speedMod = 1f + 1f * _currentEagleSpeedMod;
            if (speedMod > eagleSpeedModMax)
                speedMod = eagleSpeedModMax;
            eagle.Setup(5f, speedMod);
            eagle.SetTarget(target.transform);
            eagle.MoveToTarget();
            
            audioManager.PlayEagleSpawn();
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
            _started = true;

            StartCoroutine(IncreaseSpawnTimeRoutine());
        }

        private IEnumerator IncreaseSpawnTimeRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnTimeDecreaseDelay);
                _spawnTime -= spawnTimeDecrease;
                if(_spawnTime < minSpawnTime)
                    _spawnTime = minSpawnTime;
            }
        }

        private void StopSpawner()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}