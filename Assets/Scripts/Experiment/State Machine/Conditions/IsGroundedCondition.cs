using UnityEngine;

public class IsGroundedCondition : ConditionLogic
{
    [SerializeField] private GroundCheckerBase _groundChecker;

    private void Awake()
    {
        _groundChecker = GetComponentInParent<GroundCheckerBase>();
    }

    public override bool IsMet()
    {
        if (_groundChecker.GetGrounded())
        {
            return true;
        }

        return false;
    }
}
