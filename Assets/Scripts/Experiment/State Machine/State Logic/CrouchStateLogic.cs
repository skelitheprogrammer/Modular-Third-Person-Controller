public class CrouchStateLogic : StateLogic
{
    [UnityEngine.SerializeField] private PlayerJumping _jumping;

    public override void Enter()
    {
        _jumping.enabled = false;
    }

    public override void Exit()
    {
        _jumping.enabled = true;
    }
}
