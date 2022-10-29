using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lost : State
{
    public Lost(GameplayManager gameplayManager) : base(gameplayManager)
    {
    }

    public override IEnumerator Start()
    {
        GameplayManager.storyText.text = "You lost the game...";
        yield return new WaitForSeconds(GameplayManager.dialogueDelay);
        SceneManager.LoadScene("GameOver");
    }
}