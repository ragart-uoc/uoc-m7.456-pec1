using System;
using System.Collections;
using UnityEngine;
using PEC1.GameManagers;
using Random = UnityEngine.Random;

namespace PEC1.BattleStates
{
    public class PlayerTurn : State
    {
        public PlayerTurn(CombatManager combatManager) : base(combatManager)
        {
        }

        public override IEnumerator Start()
        {
            CombatManager.enemyText.text = String.Empty;
            CombatManager.playerText.text = String.Empty;
            CombatManager.FillInsults();
            yield break;
        }

        public override IEnumerator Insult(int insultIndex)
        {
            CombatManager.DestroyInsultComebacksOnScreen();
            CombatManager.enemyText.text = String.Empty;
            CombatManager.playerText.text = CombatManager.insultComebacks[insultIndex].insult;
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            var enemyComebackIndex = Random.Range(0, CombatManager.insultComebacks.Length);
            if (Random.value > 0.66f) enemyComebackIndex = insultIndex;
            var enemyComeback = CombatManager.insultComebacks[enemyComebackIndex].comeback;
            CombatManager.enemyText.text = enemyComeback;
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            if (enemyComebackIndex == insultIndex)
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
            else
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
        }
    }
}