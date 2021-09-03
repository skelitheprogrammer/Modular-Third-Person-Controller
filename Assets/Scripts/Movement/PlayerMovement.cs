using NaughtyAttributes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovementValue, IModule
{
    [Header("Movement settings")]
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _acceleration = 40f;
    [SerializeField] private float _groundMoveForce = .3f;
    [SerializeField] private float _airStrafeForce = .7f;

    private Vector3 _smoothVelocity;
    public Vector3 Velocity { get; private set; }

    [ShowNonSerializedField] private float _currentSpeed;

    [ShowNativeProperty] public Vector3 MovementDirection { get; private set; }
    [ShowNativeProperty] public Vector3 AirDirection { get; private set; }

    [ShowNativeProperty] public Vector3 AirDirectionTransformed { get; private set; }

    [ShowNativeProperty] public Vector3 Value { get; private set; }

    private IMovementHandler _movementHandler;
    private IModuleHandler _moduleHandler;
    private GroundCheckerBase _groundCheck;

    private InputReader _input;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _movementHandler = GetComponent<IMovementHandler>();
        _moduleHandler = GetComponent<IModuleHandler>();
        _groundCheck = GetComponent<GroundCheckerBase>();
    }

    private void OnEnable()
    {
        _movementHandler.MovementObserver.Subscribe(this);
        _moduleHandler.ModuleObserver.Subscribe(this);
    }

    private void OnDisable()
    {
        _movementHandler.MovementObserver.Unsubscribe(this);
        _moduleHandler.ModuleObserver.Unsubscribe(this);
    }

    public void OnUpdateModule()
    {
        Move();
    }

    private void Move()
    {
        Vector3 inputDirection = new Vector3(_input.MoveInput.x, 0, _input.MoveInput.y);

        if (_groundCheck.GetGrounded())
        {

            float targetSpeed = _speed * inputDirection.magnitude;

            MovementDirection = inputDirection;
            MovementDirection = transform.TransformDirection(MovementDirection);
            
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _acceleration * Time.deltaTime);

            if (AirDirection != Vector3.zero)
            {
                AirDirection = Vector3.zero;
                AirDirectionTransformed = Vector3.zero;
            }
        }
        else
        {
            if (inputDirection != Vector3.zero)
            {
                AirDirection += inputDirection;
                AirDirectionTransformed = transform.TransformDirection(AirDirection);
            }
        }

        Vector3 finalVelocity = (MovementDirection + AirDirectionTransformed).normalized * _currentSpeed;

        if (finalVelocity != Vector3.zero)
        {
            float finalSmooth = _groundCheck.GetGrounded() ? _groundMoveForce : _airStrafeForce;
            Velocity = Vector3.SmoothDamp(Velocity, finalVelocity, ref _smoothVelocity, finalSmooth);
        }
        else
        {
            Velocity = Vector3.MoveTowards(Velocity, Vector3.zero, _acceleration * Time.deltaTime);
            _smoothVelocity = Vector3.zero;
        }

        Value = Velocity;
    }
}