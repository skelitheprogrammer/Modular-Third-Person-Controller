using UnityEngine;

[System.Serializable]
public class StateTransition
{
    [SerializeField] private string _nextStateName = "Next state name";

    [SerializeField] private State _nextState;
    public State NextState => _nextState;

    [SerializeField] private StateCondition[] _conditions;

    public bool ShouldTransition()
    {
        foreach(var item in _conditions)
        {
            if (item.ShouldBe != item.Logic.IsMet())
            {
                return false;
            }
        }

        return true;
    }
    
}
