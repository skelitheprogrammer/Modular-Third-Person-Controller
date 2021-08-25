using UnityEngine;

[System.Serializable]
public class StateCondition
{
    [SerializeField] private string _conditionName;

    [SerializeField] private bool _shouldBe;
    public bool ShouldBe => _shouldBe;

    [SerializeField] private ConditionLogic _logic;
    public ConditionLogic Logic => _logic;
}
