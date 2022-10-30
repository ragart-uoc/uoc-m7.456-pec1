using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace PEC1.GameManagers
{
    public class IntroManager : MonoBehaviour
    {
        
        public TextMeshProUGUI playerText;
        public TextMeshProUGUI enemyText;
        public TextMeshProUGUI storyText;

        private float dialogueSpeed = 3.0f;
        
        private IEnumerator Start()
        {
            playerText.text = String.Empty;
            enemyText.text = String.Empty;
            storyText.text = "Deep in the Caribbean...";
            yield return new WaitForSeconds(dialogueSpeed);
            storyText.text = String.Empty;
            playerText.text = "My name is Dudebrush Threepmetal. You killed my father! Prepare to...!";
            yield return new WaitForSeconds(dialogueSpeed);
            playerText.text = String.Empty;
            enemyText.text = "Woah, woah, WOAH!";
            yield return new WaitForSeconds(dialogueSpeed);
            enemyText.text = "I had never seen  so many infringements of intellectual property in one place!";
            yield return new WaitForSeconds(dialogueSpeed);
            enemyText.text = String.Empty;
            playerText.text = "What do you mean? This is an original IP!";
            yield return new WaitForSeconds(dialogueSpeed);
            playerText.text = String.Empty;
            enemyText.text = "You even ripped the sprites from that ancient game that no one remembers at all!";
            yield return new WaitForSeconds(dialogueSpeed);
            enemyText.text = "This is outrageous! En garde!";
            yield return new WaitForSeconds(dialogueSpeed);
            SceneManager.LoadScene("MainMenu");
        }
        
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
