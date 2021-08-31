using UnityEngine;

[System.Serializable]
public class StateCondition
{
#if UNITY_EDITOR
    [SerializeField] private string _conditionName;
#endif

    [SerializeField] private bool _shouldBe;
    public bool ShouldBe => _shouldBe;

    [SerializeField] private StateConditionLogic _logic;

    public StateConditionLogic ConditionLogic => _logic;
}
