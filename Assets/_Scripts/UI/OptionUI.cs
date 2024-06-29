using System;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Managers;
using TMPro;

namespace _Scripts.UI
{
    public class OptionUI : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;

        private TextMeshProUGUI _musicText;
        private TextMeshProUGUI _soundVolumeText;
        private TextMeshProUGUI _musicVolumeText;
        private void Awake()
        {
            _soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
            _musicVolumeText = transform.Find("musicVolumeText").GetComponent<TextMeshProUGUI>();
            
            OnClickBtn();
        }

        private void OnClickBtn()
        {
            transform.Find("soundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                soundManager.IncreaseVolume();
                UpdateSoundText();
            });

            transform.Find("soundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                soundManager.DecreaseVolume();
                UpdateSoundText();
            });

            transform.Find("musicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                MusicManager.Instance.IncreaseVolume();
                UpdateMusicText();
            });

            transform.Find("musicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                MusicManager.Instance.DecreaseVolume();
                UpdateMusicText();
            });
            
            transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
               Time.timeScale = 1;
               GameSceneManager.LoadScene(GameSceneManager.Scene.MainMenuScene);
            });
        }

        private void Start()
        {
            UpdateSoundText();
            UpdateMusicText();
            gameObject.SetActive(false);
        }

        private void UpdateSoundText()
        {
            _soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetVolume() * 10).ToString());
        }
        
        private void UpdateMusicText()
        {
            _musicVolumeText.SetText(Mathf.RoundToInt(MusicManager.Instance.GetVolume() * 10).ToString());
        }

        public void ToggleVisible()
        {
            gameObject.SetActive(!gameObject.activeSelf);
            Time.timeScale = gameObject.activeSelf ? 0 : 1;
        }
    }
}
