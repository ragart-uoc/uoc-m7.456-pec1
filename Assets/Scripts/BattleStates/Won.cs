using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Won : State
{
    public Won(GameplayManager gameplayManager) : base(gameplayManager)
    {
    }

    public override IEnumerator Start()
    {
        GameplayManager.storyText.text = "You won the game!";
        yield return new WaitForSeconds(GameplayManager.dialogueDelay);
        SceneManager.LoadScene("GameOver");
    }
}