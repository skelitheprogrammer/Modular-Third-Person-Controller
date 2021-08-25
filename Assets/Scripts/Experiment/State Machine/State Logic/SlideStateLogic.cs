using UnityEngine;

public class SlideStateLogic : StateLogic
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerRotation _rotation;

    public override void Enter()
    {
        _movement.enabled = false;
        _rotation.enabled = false;
    }

    public override void Exit()
    {
        _movement.enabled = true;
        _rotation.enabled = true;
    }
}
