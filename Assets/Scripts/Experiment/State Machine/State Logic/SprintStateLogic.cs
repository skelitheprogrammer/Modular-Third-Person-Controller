public class SprintStateLogic : StateLogic
{
    [UnityEngine.SerializeField] private PlayerCrouching _crouching;
    public override void Enter()
    {
        _crouching.enabled = false;
    }

    public override void Exit()
    {
        _crouching.enabled = true;
    }
}