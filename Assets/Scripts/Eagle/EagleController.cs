using UnityEngine;
using Worm;

namespace Eagle
{
    public class EagleController : MonoBehaviour
    {
        public Rigidbody2D rb2D;
        public float speed;
        public float speedMod;
    
        private Transform target;
        private bool alreadyHit;

        public void Setup(float speed, float speedMod)
        {
            this.speed = speed;
            this.speedMod = speedMod;
        }
    
        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void MoveToTarget()
        {
            var direction = target.position - transform.position;
            MoveToDirection(direction.normalized);
        }
    
        public void MoveToDirection(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
            rb2D.velocity = direction * (speed * speedMod);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                if (alreadyHit) return;
                
                alreadyHit = true;
                if (health.TryGetComponent(out WormController worm))
                {
                    if (!worm._isSafe)
                        health.Damage();
                }
                
                var direction = transform.right;
                direction.y *= -1;
                MoveToDirection(direction);
            }

            if (other.gameObject.CompareTag("DeadLine"))
                Destroy(gameObject);
        }
    }
}