using System.Linq;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public WormSpawner WormSpawner { get; private set; }
        [SerializeField] private EndGameController endGameController;
    
        public event System.Action OnGameEnd = delegate { };
    
        private void Start()
        {
            SpawnWorms();
        }

        private void SpawnWorms()
        {
            var playerAlreadySpawned = false;
            for (int i = 0; i < 4; i++)
            {
                var worm = WormSpawner.SpawnWorm(playerAlreadySpawned);
                worm.Health.OnDeath += CheckWormDeath;
            
                playerAlreadySpawned = true;
            }
        }

        private void CheckWormDeath()
        {
            var worms = WormSpawner.Worms;
            var wormsAlive = worms.Where(w => !w.IsDead).ToArray();
            if (wormsAlive.Count() <= 1)
            {
                var worm = wormsAlive.FirstOrDefault();
                endGameController.ShowWindow(worm?.WormName);
            
                OnGameEnd();
            }
        }
    }
}