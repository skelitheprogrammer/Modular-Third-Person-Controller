using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 RotateInput { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsSprintPressed { get; private set; }
    public bool SlidePressed { get; private set; }
    public bool CrouchPressed { get; private set; }
    public bool ClimbPressed { get; private set; }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        RotateInput = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        IsJumpPressed = ctx.started;
    }
}
