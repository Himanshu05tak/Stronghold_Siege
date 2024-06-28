using UnityEngine.SceneManagement;

namespace _Scripts.Managers
{
    public static class GameSceneManager
    {
        public enum Scene
        {
            GameScene,
            MainMenuScene
        }
        public static void LoadScene(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}
