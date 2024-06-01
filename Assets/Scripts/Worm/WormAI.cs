using Eagle;
using UnityEngine;

namespace Worm
{
    public class WormAI : WormInput
    {
        public CircleCollider2D Sensor { get; private set; }
    
        public void Setup(float sensorSize)
        {
            Sensor = gameObject.AddComponent<CircleCollider2D>();
            Sensor.radius = sensorSize;
            Sensor.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("collide ai");
            if (other.gameObject.TryGetComponent(out EagleController eagle))
            {
                Debug.Log("eagleeee");
                WormController.Hide();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EagleController eagle))
            {
                WormController.Unhide();
            }
        }
    }
}