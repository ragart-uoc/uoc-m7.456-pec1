using UnityEngine;
using UnityEngine.SceneManagement;

namespace PEC1.GameManagers
{
    public class GameOverManager : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Combat");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}