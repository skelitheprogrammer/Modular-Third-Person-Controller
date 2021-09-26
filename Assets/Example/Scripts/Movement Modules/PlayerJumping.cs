using UnityEngine;

public class PlayerJumping : MonoBehaviour, IMovementModule
{
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _jumpTimeout = .3f;

    private float _jumpTimeoutDelta;

    private float _currentJumpHeight;

    [SerializeField] private InputReader _input;
    [SerializeField] private GravityBase _gravity;
    [SerializeField] private GroundCheckerBase _groundCheck;
    [SerializeField] private ModuleHandlerBase<IMovementModule> _moduleHandler;

    public Vector3 Value { get; private set; }

    private void OnEnable()
    {
        _moduleHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.Unsubscribe(this);
    }

    public void OnUpdateModule()
    {
        Jump();
    }

    private void Jump()
    {
        var isGrounded = _groundCheck.IsGrounded;
        var wasGrounded = _groundCheck.WasGroundedLastframe;
        var jumpValue = Mathf.Sqrt(-2 * _jumpHeight * _gravity.Gravity);

        if (isGrounded)
        {
            if (_currentJumpHeight != 0)
            {
                _currentJumpHeight = 0;
            }

            if (_jumpTimeoutDelta > 0)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }

            if (_input.IsJumpPressed && _jumpTimeoutDelta <= 0)
            {
                _currentJumpHeight = jumpValue;
                _currentJumpHeight += 10f;
            }
        }
        else if (wasGrounded)
        {
            if (_currentJumpHeight > 0)
            {
                _currentJumpHeight -= 10;
            }
        }
        else
        {

            if (_jumpTimeoutDelta != _jumpTimeout)
            {
                _jumpTimeoutDelta = _jumpTimeout;
            }
        }

        Value = Vector3.up * _currentJumpHeight;
    }
}
