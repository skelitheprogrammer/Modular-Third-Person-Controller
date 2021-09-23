using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovementModule
{
    [SerializeField] private ModuleHandlerBase<IMovementModule> _moduleHandler;

    [Header("Movement settings")]
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _acceleration = 40f;
    [SerializeField] private float _groundMoveForce = .3f;
    [SerializeField] private float _airStrafeForce = .7f;

    private float _currentSpeed;
    public float CurrentSpeed => _currentSpeed;
    public float TargetSpeed { get; private set; }
    
    private Transform _camera;
    private Vector3 _smoothVelocity;

    private Vector3 _velocity;

    private Vector3 _movementDirection;
    private Vector3 _airDirection;
    private Vector3 _airDirectionTransformed;

    public Vector3 Value { get; private set; }

    [SerializeField] private GroundCheckerBase _groundCheck;
    [SerializeField] private InputReader _input;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

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
        Move();
    }

    private void Move()
    {
        var inputDirection = new Vector3(_input.MoveInput.x, 0, _input.MoveInput.y);

        var isGrounded = _groundCheck.IsGrounded;
        var finalSmooth = isGrounded ? _groundMoveForce : _airStrafeForce;

        TargetSpeed = _moveSpeed * inputDirection.magnitude;

        if (isGrounded)
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, TargetSpeed, _acceleration * Time.deltaTime);

            _movementDirection = Quaternion.Euler(0, _camera.eulerAngles.y, 0) * inputDirection;

            if (_airDirection != Vector3.zero)
            {
                _airDirection = Vector3.zero;
                _airDirectionTransformed = Vector3.zero;
            }
        }
        else
        {
            if (inputDirection != Vector3.zero)
            {
                _airDirection += inputDirection;
                _airDirectionTransformed = _camera.TransformDirection(_airDirection);
            }
        }

        var finalVelocity = (_movementDirection + _airDirectionTransformed).normalized * _currentSpeed;
        finalVelocity.y = 0;

        _velocity = Vector3.SmoothDamp(_velocity, finalVelocity, ref _smoothVelocity, finalSmooth);

        Value = _velocity;
    }
}
