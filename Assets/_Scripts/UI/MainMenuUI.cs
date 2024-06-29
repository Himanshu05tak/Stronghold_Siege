using UnityEngine;
using UnityEngine.UI;
using _Scripts.Managers;

namespace _Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        private void Awake()
        {
            transform.Find("playBtn").GetComponent<Button>().onClick.AddListener(() => {GameSceneManager.LoadScene(GameSceneManager.Scene.GameScene); });

            transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(Application.Quit);
        }
    }
}
