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
            CombatManager.storyText.text = "You lost the game...";
            yield return new WaitForSeconds(CombatManager.dialogueDelay);
            SceneManager.LoadScene("GameOver");
        }
    }
}