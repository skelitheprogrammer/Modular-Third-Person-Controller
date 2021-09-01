using NaughtyAttributes;
using UnityEngine;

public class PlayerJumping : MonoBehaviour, IMovementValue, IModule
{
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _jumpTimeout = .3f;

    private float _jumpTimeoutDelta;

    private float _currentJumpHeight;

    private InputReader _input;
    private GravityBase _gravity;
    private GroundCheckerBase _groundCheck;
    private IMovementHandler _handler;
    private IModuleHandler _moduleHandler;

    [ShowNativeProperty] public Vector3 Value { get; private set; }

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _gravity = GetComponent<GravityBase>();   
        _groundCheck = GetComponent<GroundCheckerBase>();
        _handler = GetComponent<IMovementHandler>();
        _moduleHandler = GetComponent<IModuleHandler>();

    }

    private void OnEnable()
    {
        _handler.Subscribe(this);
        _moduleHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        _handler.UnSubscribe(this);
        _moduleHandler.UnSubscribe(this);
    }

/*    private void Update()
    {
        Jump();
    }*/

    public void OnUpdateModule()
    {
        Jump();
    }
    private void Jump()
    {
        if (_groundCheck.GetGrounded())
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
                _currentJumpHeight = Mathf.Sqrt(2 * _jumpHeight * _gravity.Gravity);
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
