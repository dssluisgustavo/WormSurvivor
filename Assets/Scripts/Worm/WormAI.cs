using System.Collections;
using Eagle;
using UnityEngine;

namespace Worm
{
    public class WormAI : WormInput
    {
        public CircleCollider2D Sensor { get; private set; }
        public float ReactionDelay { get; private set; }

        private Coroutine _reactingCoroutine;

        public void Setup(float sensorSize, float reactionDelay)
        {
            Sensor = gameObject.AddComponent<CircleCollider2D>();
            Sensor.radius = sensorSize;
            Sensor.isTrigger = true;

            ReactionDelay = reactionDelay;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (WormController.IsDead) return;

            if (other.gameObject.TryGetComponent(out EagleController eagle))
            {
                if (_reactingCoroutine == null)
                    _reactingCoroutine = StartCoroutine(React());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (WormController.IsDead) return;

            if (other.gameObject.TryGetComponent(out EagleController eagle))
            {
                WormController.Unhide();
                if(_reactingCoroutine != null)
                    StopCoroutine(_reactingCoroutine);
                _reactingCoroutine = null;
            }
        }

        private IEnumerator React()
        {
            yield return new WaitForSeconds(Random.Range(0f, ReactionDelay));
            WormController.Hide();
            _reactingCoroutine = null;
        }
    }
}