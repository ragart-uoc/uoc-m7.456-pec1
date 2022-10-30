using UnityEngine;
using UnityEngine.SceneManagement;

namespace PEC1.GameManagers
{
    public class PauseScreenManager : MonoBehaviour
    {
        public GameObject pauseScreen;

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                pauseScreen.SetActive(!pauseScreen.activeSelf);
        }
    }
}