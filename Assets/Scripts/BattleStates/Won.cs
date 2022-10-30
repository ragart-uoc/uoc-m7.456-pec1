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
            CombatManager.playerText.text = String.Empty;
            CombatManager.enemyText.text = "I surrender!";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            SceneManager.LoadScene("GameOver");
        }
    }
}