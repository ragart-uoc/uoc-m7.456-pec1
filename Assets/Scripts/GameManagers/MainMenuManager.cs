using UnityEngine;
using UnityEngine.SceneManagement;

namespace PEC1.GameManagers
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject credits;
        
        public void StartGame()
        {
            SceneManager.LoadScene("Combat");
        }

        public void ToggleCredits()
        {
            credits.SetActive(!credits.activeSelf);
        }
        
        public void QuitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        
        private void Update()
        {
            if (credits.activeSelf && Input.GetKeyUp(KeyCode.Escape))
                ToggleCredits();
        }
    }
}
