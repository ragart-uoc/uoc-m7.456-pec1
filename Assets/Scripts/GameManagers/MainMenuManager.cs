using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PEC1.GameManagers
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject credits;
        public ScrollRect creditsScrollRect;

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
            if (!credits.activeSelf)
                return;
            if (creditsScrollRect.verticalNormalizedPosition > 0)
                creditsScrollRect.verticalNormalizedPosition -= Time.deltaTime * 0.1f;
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ToggleCredits();
                creditsScrollRect.verticalNormalizedPosition = 1;
            }
        }
    }
}
