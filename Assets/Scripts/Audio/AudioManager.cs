using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip eagleSpawn;
        [SerializeField] private AudioClip wormHit;
        [SerializeField] private AudioClip wormDeath;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayClick() => PlaySfx(click);
        public void PlayEagleSpawn() => PlaySfx(eagleSpawn);
        public void PlayWormHit() => PlaySfx(wormHit);
        public void PlayWormDeath() => PlaySfx(wormDeath);

        private void PlaySfx(AudioClip clip) => _audioSource.PlayOneShot(clip);
    }
}
