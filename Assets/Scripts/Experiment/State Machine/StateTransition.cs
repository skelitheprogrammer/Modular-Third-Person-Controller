using UnityEngine;

[System.Serializable]
public class StateTransition
{
#if UNITY_EDITOR
    [SerializeField] private string _transitionName;
#endif

    [SerializeField] private State _nextState;
    public State NextState => _nextState;

    [SerializeField] private StateCondition[] _conditions;

    public bool ShouldTransition()
    {
        foreach (var item in _conditions)
        {
            if (item.ConditionLogic.IsMet() != item.ShouldBe)
            {
                return false;
            }
        }

        return true;
    }

}
