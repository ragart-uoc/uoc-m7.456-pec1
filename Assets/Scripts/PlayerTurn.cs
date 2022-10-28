﻿using System.Collections;
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
        GameplayManager.DestroyInsultComebacksOnScreen();
        GameplayManager.storyText.text = "[PLAYER]\n" + GameplayManager.insultComebacks[insultIndex].insult;
        yield return new WaitForSeconds(GameplayManager.dialogueDelay);
        var enemyComebackIndex = Random.Range(0, GameplayManager.insultComebacks.Length);
        if (Random.value > 0.66f) enemyComebackIndex = insultIndex;
        var enemyComeback = GameplayManager.insultComebacks[enemyComebackIndex].comeback;
        GameplayManager.storyText.text = "[ENEMY]\n" + enemyComeback;
        yield return new WaitForSeconds(GameplayManager.dialogueDelay);
        if (enemyComebackIndex == insultIndex)
        {
            GameplayManager.storyText.text = "You lost a life!";
            yield return new WaitForSeconds(GameplayManager.dialogueDelay);
            if (GameplayManager.Player.TakeDamage(1))
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
            GameplayManager.storyText.text = "Your opponent lost a life.";
            yield return new WaitForSeconds(GameplayManager.dialogueDelay);
            if (GameplayManager.Enemy.TakeDamage(1))
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