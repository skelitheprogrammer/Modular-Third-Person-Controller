public class StateMachine
{
    public State CurrentState { get; private set; }

    public StateMachine(State currentState)
    {
        CurrentState = currentState;
    }

    public void Tick()
    {
        State nextState = CurrentState.ProcessTransitions();

        if (nextState != null)
        {
            ChangeState(nextState);
        }
    }

    public void ChangeState(State newState)
    {
        CurrentState.Logic?.Exit();

        CurrentState = newState;

        CurrentState.Logic?.Enter();
    }
}
