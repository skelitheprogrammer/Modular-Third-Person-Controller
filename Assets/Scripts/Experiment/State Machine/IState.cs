public interface IState
{
    StateTransition[] Transitions { get; }
    StateLogic Logic { get; }
    IState ProcessTransitions();
}