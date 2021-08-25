using UnityEngine;

public class IsSlidingCondition : ConditionLogic
{
    [SerializeField] private GroundCheckerBase _groundChecker;
    public override bool IsMet()
    {
        if (_groundChecker.GetGrounded())
        {
            return true;
        }

        return false;
    }
}
