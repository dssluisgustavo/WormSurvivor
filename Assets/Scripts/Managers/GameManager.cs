using System.Collections;
using System.Linq;
using DG.Tweening;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public WormSpawner WormSpawner { get; private set; }
        [SerializeField] private EndGameController endGameController;
        [SerializeField] private GamePresentation gamePresentation;
    
        public event System.Action OnGameStart = delegate { };
        public event System.Action OnGameEnd = delegate { };
    
        private IEnumerator Start()
        {
            SpawnWorms();
            
            yield return new WaitForSeconds(2f);
            yield return gamePresentation.ShowPresentation().WaitForCompletion();
            
            //show instructions
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
            WormSpawner.FixCharacterHUDOrder();
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