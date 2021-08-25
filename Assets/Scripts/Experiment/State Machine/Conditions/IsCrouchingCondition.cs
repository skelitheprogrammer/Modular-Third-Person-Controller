using UnityEngine;

public class IsCrouchingCondition : ConditionLogic
{
    [SerializeField] private PlayerCrouching _crouching;

    public override bool IsMet()
    {
        if (_crouching.IsCrouching)
        {
            return true;
        }

        return false;
    }
}
