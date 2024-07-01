using UnityEngine;

namespace _Scripts.Managers
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        private AudioSource _audioSource;
        private float _volume;
        
        private const string MUSIC = "MUSIC_VOLUME";

        private void Awake()
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _volume = PlayerPrefs.GetFloat(MUSIC, 0.5f);
            _audioSource.volume = _volume;
        }

        public void IncreaseVolume()
        {
            _volume += .1f;
            _volume = Mathf.Clamp01(_volume);
            _audioSource.volume = _volume;
            PlayerPrefs.SetFloat(MUSIC,_volume);
        }
        
        public void DecreaseVolume()
        {
            _volume -= .1f;
            _volume = Mathf.Clamp01(_volume);
            _audioSource.volume = _volume;
            PlayerPrefs.SetFloat(MUSIC,_volume);
        }
        public float GetVolume()
        {
            return _volume;
        }
    }
}
