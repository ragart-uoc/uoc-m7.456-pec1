using System.Collections;
using UnityEngine;
using PEC1.GameManagers;

namespace PEC1.BattleStates
{
    public class PlayerTurn : State
    {
        public PlayerTurn(CombatManager combatManager) : base(combatManager)
        {
        }

        public override IEnumerator Start()
        {
            CombatManager.storyText.text = "Choose an insult";
            CombatManager.FillInsults();
            yield break;
        }

        public override IEnumerator Insult(int insultIndex)
        {
            CombatManager.DestroyInsultComebacksOnScreen();
            CombatManager.storyText.text = "[PLAYER]\n" + CombatManager.insultComebacks[insultIndex].insult;
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            var enemyComebackIndex = Random.Range(0, CombatManager.insultComebacks.Length);
            if (Random.value > 0.66f) enemyComebackIndex = insultIndex;
            var enemyComeback = CombatManager.insultComebacks[enemyComebackIndex].comeback;
            CombatManager.storyText.text = "[ENEMY]\n" + enemyComeback;
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            if (enemyComebackIndex == insultIndex)
            {
                CombatManager.storyText.text = "You lost a life!";
                yield return new WaitForSeconds(CombatManager.dialogueDelay);
                if (CombatManager.Player.TakeDamage(1))
                {
                    CombatManager.SetState(new Lost(CombatManager));
                }
                else
                {
                    CombatManager.SetState(new EnemyTurn(CombatManager));
                }
            }
            else
            {
                CombatManager.storyText.text = "Your opponent lost a life.";
                yield return new WaitForSeconds(CombatManager.dialogueDelay);
                if (CombatManager.Enemy.TakeDamage(1))
                {
                    CombatManager.SetState(new Won(CombatManager));
                }
                else
                {
                    CombatManager.SetState(new PlayerTurn(CombatManager));
                }
            }
        }
    }
}