using Managers;
using UnityEngine;
using Worm;

namespace Eagle
{
    public class EagleController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private EagleSpriteHandler eagleSpriteHandler;
        [SerializeField] private float speed;
        [SerializeField] private float speedMod;
    
        private Transform _target;
        private bool _alreadyHit;

        private GameManager _gameManager;

        private void OnDisable()
        {
            if(_gameManager)
                _gameManager.OnGameEnd -= KillObject;
        }

        public void SetGameManager(GameManager gameManager)
        {
            this._gameManager = gameManager;
            gameManager.OnGameEnd += KillObject;
        }
        
        public void Setup(float speed, float speedMod)
        {
            this.speed = speed;
            this.speedMod = speedMod;
        }
    
        public void SetTarget(Transform target)
        {
            this._target = target;
        }

        public void MoveToTarget()
        {
            var direction = _target.position - transform.position;
            MoveToDirection(direction.normalized);
        }
    
        public void MoveToDirection(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
            rb2D.velocity = direction * (speed * speedMod);
            eagleSpriteHandler.FixRotation(rb2D.velocity.x);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                if (_alreadyHit) return;
                
                _alreadyHit = true;
                if (health.TryGetComponent(out WormController worm))
                {
                    if (!worm.IsSafe)
                        health.Damage();
                }
                
                eagleSpriteHandler.ChangeToRisingSprite();
                
                var direction = transform.right;
                direction.y *= -1;
                MoveToDirection(direction);
            }

            if (other.gameObject.CompareTag("DeadLine"))
                Destroy(gameObject);
        }
        
        private void KillObject()
        {
            Destroy(gameObject);
        }
    }
}