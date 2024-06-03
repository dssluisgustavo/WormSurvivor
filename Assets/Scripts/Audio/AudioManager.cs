using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip eagleSpawn;
        [SerializeField] private AudioClip wormHit;
        [SerializeField] private AudioClip wormDie;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayClick() => PlaySfx(click);
        public void PlayEagleSpawn() => PlaySfx(eagleSpawn);
        public void PlayWormHit() => PlaySfx(wormHit);
        public void PlayWormDie() => PlaySfx(wormDie);

        private void PlaySfx(AudioClip clip) => _audioSource.PlayOneShot(clip);
    }
}
