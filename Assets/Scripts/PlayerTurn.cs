using System.Collections;
using UnityEngine;

public class PlayerTurn : State
{
    public PlayerTurn(GameplayManager gameplayManager) : base(gameplayManager)
    {
    }
    
    public override IEnumerator Start()
    {
        GameplayManager.storyText.text = "Choose an insult";
        GameplayManager.FillInsults();
        yield break;
    }
    
    public override IEnumerator Insult(int insultIndex)
    {
        GameplayManager.DestroyInsultComebacks();
        GameplayManager.storyText.text = "[PLAYER]\n" + GameplayManager.insultComebacks[insultIndex].insult;
        yield return new WaitForSeconds(GameplayManager.dialogueDelay);
        var enemyComebackIndex = Random.Range(0, GameplayManager.insultComebacks.Length);
        var enemyComeback = GameplayManager.insultComebacks[enemyComebackIndex].comeback;
        GameplayManager.storyText.text = "[ENEMY]\n" + enemyComeback;
        yield return new WaitForSeconds(GameplayManager.dialogueDelay);
        if (enemyComebackIndex == insultIndex)
        {
            GameplayManager.playerLives--;
            GameplayManager.storyText.text = "You lost a life!";
            yield return new WaitForSeconds(GameplayManager.dialogueDelay);
            if (GameplayManager.playerLives == 0)
            {
                GameplayManager.SetState(new Lost(GameplayManager));
            }
            else
            {
                GameplayManager.SetState(new EnemyTurn(GameplayManager));
            }
        }
        else
        {
            GameplayManager.enemyLives--;
            GameplayManager.storyText.text = "Your opponent lost a life.";
            yield return new WaitForSeconds(GameplayManager.dialogueDelay);
            if (GameplayManager.enemyLives == 0)
            {
                GameplayManager.SetState(new Won(GameplayManager));
            }
            else
            {
                GameplayManager.SetState(new PlayerTurn(GameplayManager));
            }
        }
    }
}