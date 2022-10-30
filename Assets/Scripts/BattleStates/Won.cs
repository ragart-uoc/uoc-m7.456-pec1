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
            CombatManager.storyText.text = "You won the game!";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            SceneManager.LoadScene("GameOver");
        }
    }
}