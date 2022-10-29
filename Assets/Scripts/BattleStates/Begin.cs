using System.Collections;
using UnityEngine;

public class Begin : State
{
    public Begin(GameplayManager gameplayManager) : base(gameplayManager)
    {
    }
    
    public override IEnumerator Start()
    {
        if (Random.value > 0.5f)
        {
            GameplayManager.storyText.text = "[PLAYER] I'll start!";
            yield return new WaitForSeconds(GameplayManager.dialogueDelay);
            GameplayManager.SetState(new PlayerTurn(GameplayManager));
        }
        else
        {
            GameplayManager.storyText.text = "[ENEMY] It's my turn!";
            yield return new WaitForSeconds(GameplayManager.dialogueDelay);
            GameplayManager.SetState(new EnemyTurn(GameplayManager));
        }
    }
}