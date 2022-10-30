using System.Collections;
using PEC1.GameManagers;

namespace PEC1.BattleStates
{
    public abstract class State
    {
        protected CombatManager CombatManager;

        public State(CombatManager combatManager)
        {
            CombatManager = combatManager;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Insult(int insultIndex)
        {
            yield break;
        }

        public virtual IEnumerator Comeback(int insultIndex, int comebackIndex)
        {
            yield break;
        }
    }
}