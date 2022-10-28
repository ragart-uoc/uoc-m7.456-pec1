using System.Collections;
using UnityEngine;

public class EnemyTurn : State
{
    public EnemyTurn(GameplayManager gameplayManager) : base(gameplayManager)
    {
    }
    
    public override IEnumerator Start()
    {
        var enemyInsultIndex = Random.Range(0, GameplayManager.insultComebacks.Length);
        var enemyInsult = GameplayManager.insultComebacks[enemyInsultIndex].insult;
        GameplayManager.storyText.text = "[ENEMY]\n" + enemyInsult;
        GameplayManager.FillComebacks(enemyInsultIndex);
        yield break;
    }
    
    public override IEnumerator Comeback(int insultIndex, int comebackIndex)
    {
        GameplayManager.DestroyInsultComebacks();
        GameplayManager.storyText.text = "[PLAYER]\n" + GameplayManager.insultComebacks[comebackIndex].comeback;
        yield return new WaitForSeconds(GameplayManager.dialogueDelay);
        if (insultIndex == comebackIndex)
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
        else
        {
            GameplayManager.playerLives--;
            GameplayManager.storyText.text = "You lost a life.";
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
    }
}