using UnityEngine;

public class SlideStateLogic : StateLogic
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerCrouching _crouching;
    [SerializeField] private PlayerJumping _jumping;

    public override void Enter()
    {
        _movement.enabled = false;
        _crouching.enabled = false;
        _jumping.enabled = false;
    }

    public override void Exit()
    {
        _movement.enabled = true;
        _crouching.enabled = true;
        _jumping.enabled = true;
    }
}
