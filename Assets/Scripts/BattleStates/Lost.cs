using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using PEC1.GameManagers;

namespace PEC1.BattleStates
{
    public class Lost : State
    {
        public Lost(CombatManager combatManager) : base(combatManager)
        {
        }

        public override IEnumerator Start()
        {
            CombatManager.mPlayerAnimator.SetTrigger("onPlayerDead");
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            CombatManager.playerText.text = String.Empty;
            CombatManager.enemyText.text = "I won! Surrender your assets!";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            CombatManager.enemyText.text = String.Empty;
            CombatManager.playerText.text = "D'oh!";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            SceneManager.LoadScene("GameOver");
        }
    }
}