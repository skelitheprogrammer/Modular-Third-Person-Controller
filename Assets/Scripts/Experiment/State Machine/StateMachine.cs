public class StateMachine
{
    public IState CurrentState { get; private set; }

    public StateMachine(IState initState)
    {
        ChangeState(initState);
    }

    public void Tick()
    {
        IState nextState = CurrentState?.ProcessTransitions();

        if (nextState != null)
        {
            ChangeState(nextState);
        }
    }

    public void ChangeState(IState newState)
    {

        CurrentState?.Logic?.Exit();

        CurrentState = newState;

        CurrentState?.Logic?.Enter();
    }
}
