using System.Linq;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public WormSpawner wormSpawner;
        public EndGameController endGameController;
    
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
                var worm = wormSpawner.SpawnWorm(playerAlreadySpawned);
                worm.health.OnDeath += CheckWormDeath;
            
                playerAlreadySpawned = true;
            }
        }

        private void CheckWormDeath()
        {
            var worms = wormSpawner.Worms;
            var wormsAlive = worms.Where(w => !w.IsDead);
            if (wormsAlive.Count() <= 1)
            {
                var worm = worms.FirstOrDefault();
                endGameController.ShowWindow(worm?.wormName);
            
                OnGameEnd();
            }
        }
    }
}