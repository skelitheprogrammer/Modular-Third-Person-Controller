using UnityEngine;

public class IsGroundedCondition : StateConditionLogic
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
