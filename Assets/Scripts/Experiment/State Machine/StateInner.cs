using UnityEngine;

[System.Serializable]
public class StateInner : IState
{
#if UNITY_EDITOR
    [SerializeField] private string _stateName;
#endif

    [SerializeField] StateTransition[] _transitions;
    public StateTransition[] Transitions => _transitions;

    [SerializeField] private StateLogic _logic;
    public StateLogic Logic => _logic;
    public IState ProcessTransitions()
    {
        foreach (var item in _transitions)
        {
            if (item.ShouldTransition())
            {
                return (IState)item.NextState;
            }
        }

        return null;
    }
}