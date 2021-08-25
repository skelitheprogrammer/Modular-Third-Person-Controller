using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] protected string _stateName;

    [SerializeField] protected StateTransition[] _transitions;

    [SerializeField] protected StateLogic _logic;
    public StateLogic Logic => _logic;

    public State ProcessTransitions()
    {
        foreach (var item in _transitions)
        {
            if (item.ShouldTransition())
            {
                return item.NextState;
            }
        }

        return null;
    }
}
