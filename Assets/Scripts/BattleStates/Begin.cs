using System.Collections;
using UnityEngine;
using PEC1.GameManagers;

namespace PEC1.BattleStates
{
    public class Begin : State
    {
        public Begin(CombatManager combatManager) : base(combatManager)
        {
        }

        public override IEnumerator Start()
        {
            if (Random.value > 0.5f)
            {
                CombatManager.storyText.text = "[PLAYER] I'll start!";
                yield return new WaitForSeconds(CombatManager.dialogueDelay);
                CombatManager.SetState(new PlayerTurn(CombatManager));
            }
            else
            {
                CombatManager.storyText.text = "[ENEMY] It's my turn!";
                yield return new WaitForSeconds(CombatManager.dialogueDelay);
                CombatManager.SetState(new EnemyTurn(CombatManager));
            }
        }
    }
}