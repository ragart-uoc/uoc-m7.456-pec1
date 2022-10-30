using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using PEC1.GameManagers;

namespace PEC1.BattleStates
{
    public class Won : State
    {
        public Won(CombatManager combatManager) : base(combatManager)
        {
        }

        public override IEnumerator Start()
        {
            CombatManager.mEnemyAnimator.SetTrigger("onEnemyDead");
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            CombatManager.enemyText.text = String.Empty;
            CombatManager.playerText.text = "Hah, I won! Take that, you corporate scum!";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            CombatManager.playerText.text = String.Empty;
            CombatManager.enemyText.text = "You're going to jail anyway, you know.";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            CombatManager.enemyText.text = String.Empty;
            CombatManager.playerText.text = "D'oh!";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            SceneManager.LoadScene("GameOver");
        }
    }
}