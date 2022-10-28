using System.Collections;

public abstract class State
{
    protected GameplayManager GameplayManager;
    
    public State(GameplayManager gameplayManager)
    {
        GameplayManager = gameplayManager;
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