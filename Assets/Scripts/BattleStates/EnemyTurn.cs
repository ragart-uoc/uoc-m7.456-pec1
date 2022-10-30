using System;
using System.Collections;
using UnityEngine;
using PEC1.GameManagers;
using Random = UnityEngine.Random;

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
            CombatManager.playerText.text = String.Empty;
            CombatManager.enemyText.text = enemyInsult;
            CombatManager.FillComebacks(enemyInsultIndex);
            yield break;
        }

        public override IEnumerator Comeback(int insultIndex, int comebackIndex)
        {
            CombatManager.DestroyInsultComebacksOnScreen();
            CombatManager.playerText.text = CombatManager.insultComebacks[comebackIndex].comeback;
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            if (insultIndex == comebackIndex)
            {
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