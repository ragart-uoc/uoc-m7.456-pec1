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
            CombatManager.enemyText.text = String.Empty;
            CombatManager.playerText.text = "I surrender!";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            SceneManager.LoadScene("GameOver");
        }
    }
}