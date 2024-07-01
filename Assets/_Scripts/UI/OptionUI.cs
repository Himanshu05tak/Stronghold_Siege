using UnityEngine;
using UnityEngine.UI;
using _Scripts.Managers;
using _Scripts.Utilities;
using TMPro;

namespace _Scripts.UI
{
    public class OptionUI : MonoBehaviour
    {
        [SerializeField] private SoundManager soundManager;

        private TextMeshProUGUI _musicText;
        private TextMeshProUGUI _soundVolumeText;
        private TextMeshProUGUI _musicVolumeText;

        private const string SOUND_VOLUME_TEXT = "soundVolumeText";
        private const string MUSIC_VOLUME_TEXT = "musicVolumeText";
        private const string SOUND_INCREASE_BTN = "soundIncreaseBtn";
        private const string SOUND_DECREASE_BTN = "soundDecreaseBtn";
        private const string MUSIC_INCREASE_TEXT = "musicIncreaseBtn";
        private const string MUSIC_DECREASE_TEXT = "musicDecreaseBtn";
        private const string MAIN_MENU_BTN = "mainMenuBtn";
        private const string EDGE_SCROLLING_BTN = "edgeScrollingBtn";
     
        
        private void Awake()
        {
            _soundVolumeText = transform.Find(SOUND_VOLUME_TEXT).GetComponent<TextMeshProUGUI>();
            _musicVolumeText = transform.Find(MUSIC_VOLUME_TEXT).GetComponent<TextMeshProUGUI>();
            
            OnClickBtn();
        }

        private void OnClickBtn()
        {
            transform.Find(SOUND_INCREASE_BTN).GetComponent<Button>().onClick.AddListener(() =>
            {
                soundManager.IncreaseVolume();
                UpdateSoundText();
            });

            transform.Find(SOUND_DECREASE_BTN).GetComponent<Button>().onClick.AddListener(() =>
            {
                soundManager.DecreaseVolume();
                UpdateSoundText();
            });

            transform.Find(MUSIC_INCREASE_TEXT).GetComponent<Button>().onClick.AddListener(() =>
            {
                MusicManager.Instance.IncreaseVolume();
                UpdateMusicText();
            });

            transform.Find(MUSIC_DECREASE_TEXT).GetComponent<Button>().onClick.AddListener(() =>
            {
                MusicManager.Instance.DecreaseVolume();
                UpdateMusicText();
            });
            
            transform.Find(MAIN_MENU_BTN).GetComponent<Button>().onClick.AddListener(() =>
            {
               Time.timeScale = 1;
               GameSceneManager.LoadScene(GameSceneManager.Scene.MainMenuScene);
            });
            transform.Find(EDGE_SCROLLING_BTN).GetComponent<Toggle>().onValueChanged.AddListener((bool set) =>
            {
                CameraHandler.Instance.SetEdgeScrolling(set);
            });
        }

        private void Start()
        {
            UpdateSoundText();
            UpdateMusicText();
            gameObject.SetActive(false);
            
            transform.Find(EDGE_SCROLLING_BTN).GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());
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
