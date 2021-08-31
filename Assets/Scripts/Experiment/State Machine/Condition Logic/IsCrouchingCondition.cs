public class IsCrouchingCondition : StateConditionLogic
{
    [UnityEngine.SerializeField] private PlayerCrouching _crouchChecker;

    public override bool IsMet()
    {
        if (_crouchChecker.IsCrouching)
        {
            return true;
        }

        return false;
    }
}
