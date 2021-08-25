using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputReader : MonoBehaviour
{
    [ShowNativeProperty] public Vector2 MoveInput { get; private set; }
    [ShowNativeProperty] public Vector2 RotateInput { get; private set; }
    [ShowNativeProperty] public bool IsJumpPressed { get; private set; }
    [ShowNativeProperty] public bool SprintPressed { get; private set; }
    [ShowNativeProperty] public bool SlidePressed { get; private set; }
    [ShowNativeProperty] public bool CrouchPressed { get; private set; }
    [ShowNativeProperty] public bool ClimbPressed { get; private set; }

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
        //IsJumpPressed = ctx.started;
        if (ctx.performed)
        {
            IsJumpPressed = true;
        }

        if (ctx.canceled)
        {
            IsJumpPressed = false;
        }
    }

    public void OnSlide(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed)
        {
            SlidePressed = true;
        }

        if (ctx.canceled)
        {
            SlidePressed = false;
        }
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SprintPressed = true;
        }

        if (ctx.canceled)
        {
            SprintPressed = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            CrouchPressed = true;
        }

        if (ctx.canceled)
        {
            CrouchPressed = false;
        }
    }

    public void OnClimb(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ClimbPressed = true;
        }

        if (ctx.canceled)
        {
            ClimbPressed = false;
        }
    }
}
