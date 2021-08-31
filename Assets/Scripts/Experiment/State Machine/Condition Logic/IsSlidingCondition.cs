using UnityEngine;
public class IsSlidingCondition : StateConditionLogic
{
    [SerializeField] private SlideCheck _slideChecker;

    public override bool IsMet()
    {
        if (_slideChecker.IsSliding)
        {
            return true;
        }

        return false;
    }
}
