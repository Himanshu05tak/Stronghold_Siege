
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Managers;

namespace _Scripts.UI
{
    public class GameOverUI : MonoBehaviour
    {
        public static GameOverUI Instance { get; private set; } 
        private void Awake()
        {
            Instance = this;
            transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(()=>
            {
                GameSceneManager.LoadScene(GameSceneManager.Scene.GameScene);
            });
            
            transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(()=>
            {
                GameSceneManager.LoadScene(GameSceneManager.Scene.MainMenuScene);
            });
        }

        private void Start()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
            transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>().SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + " Waves!");

        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
