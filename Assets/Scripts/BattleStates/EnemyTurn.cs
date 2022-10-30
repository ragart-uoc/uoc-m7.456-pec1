using System.Collections;
using UnityEngine;
using PEC1.GameManagers;

namespace PEC1.BattleStates
{
    public class EnemyTurn : State
    {
        public EnemyTurn(CombatManager combatManager) : base(combatManager)
        {
        }

        public override IEnumerator Start()
        {
            var enemyInsultIndex = Random.Range(0, CombatManager.insultComebacks.Length);
            var enemyInsult = CombatManager.insultComebacks[enemyInsultIndex].insult;
            CombatManager.storyText.text = "[ENEMY]\n" + enemyInsult;
            CombatManager.FillComebacks(enemyInsultIndex);
            yield break;
        }

        public override IEnumerator Comeback(int insultIndex, int comebackIndex)
        {
            CombatManager.DestroyInsultComebacksOnScreen();
            CombatManager.storyText.text = "[PLAYER]\n" + CombatManager.insultComebacks[comebackIndex].comeback;
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            if (insultIndex == comebackIndex)
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
            else
            {
                CombatManager.storyText.text = "You lost a life.";
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
        }
    }
}