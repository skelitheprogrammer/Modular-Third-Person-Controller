using NaughtyAttributes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovementValue
{

    [Header("Movement settings")]
    [SerializeField] private float _walkSpeed = 8f;
    [SerializeField] private float _acceleration = 40f;
    [SerializeField] private float _groundMoveForce = .3f;
    [SerializeField] private float _airStrafeForce = .7f;
   
    [Header("Sprint Settings")]
    [SerializeField] private float _sprintSpeed = 10f;
    [SerializeField] private float _endurance = 4f;
    [Range(0,1f)]
    [SerializeField] private float _enduranceTresholdPercentage = .5f;

    private Vector3 _smoothVelocity;

    [ShowNonSerializedField] private float _currentSpeed;
    [ShowNonSerializedField] private float _currentEndurance;

    [ShowNonSerializedField] private bool _isSprinting;
    [ShowNonSerializedField] private bool _sprintOnCooldown;

    [ShowNativeProperty] public Vector3 MovementDirection { get; private set; }
    [ShowNativeProperty] public Vector3 AirDirection { get; private set; }

    [ShowNativeProperty] public Vector3 AirDirectionTransformed { get; private set; }

    [ShowNativeProperty] public Vector3 Velocity { get; private set; }
    [ShowNativeProperty] public Vector3 Value { get; private set; }

    private InputReader _input;
    private IMovementHandler _handler;
    private GroundCheckerBase _groundCheck;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _handler = GetComponent<IMovementHandler>();
        _groundCheck = GetComponent<GroundCheckerBase>();
    }

    private void OnEnable()
    {
        _handler.Subscribe(this);

        _currentEndurance = _endurance;
    }

    private void OnDisable()
    {
        _handler.UnSubscribe(this);
    }

    private void Update()
    {
        Move();
        Sprint();
    }

    private void Move()
    {
        Vector3 inputDirection = new Vector3(_input.MoveInput.x, 0, _input.MoveInput.y);

        float finalSpeed = _isSprinting ? _sprintSpeed : _walkSpeed;

        float targetSpeed = finalSpeed * inputDirection.magnitude;

        if (_groundCheck.GetGrounded())
        {
            MovementDirection = inputDirection;
            MovementDirection = transform.TransformDirection(MovementDirection);
            
            AirDirection = Vector3.zero;

            _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, _acceleration * Time.deltaTime);
        }
        else
        {
            if (inputDirection != Vector3.zero)
            {
                AirDirection += inputDirection;

            }
        }

        AirDirectionTransformed = transform.TransformDirection(AirDirection);

        Vector3 finalDirection = (MovementDirection + AirDirectionTransformed).normalized * _currentSpeed;

        if (finalDirection != Vector3.zero)
        {
            float finalSmooth = _groundCheck.GetGrounded() ? _groundMoveForce : _airStrafeForce;
            Velocity = Vector3.SmoothDamp(Velocity, finalDirection, ref _smoothVelocity, finalSmooth);
        }
        else
        {
            Velocity = Vector3.MoveTowards(Velocity, Vector3.zero, _acceleration * Time.deltaTime);
            _smoothVelocity = Vector3.zero;
        }

        Value = Velocity;
    }

    private void Sprint()
    {
        if (!_isSprinting && _currentEndurance <= _endurance)
        {
            _currentEndurance += Time.deltaTime;

            if (_currentEndurance > _enduranceTresholdPercentage * _endurance)
            {
                _sprintOnCooldown = false;
            }

            if (_currentEndurance > _endurance)
            {
                _currentEndurance = _endurance;
            }
        }

        if (_input.CrouchPressed) return;

        if (Velocity != Vector3.zero)
        {
            if (_input.SprintPressed && _currentEndurance >= 0f && !_sprintOnCooldown)
            {
                _isSprinting = true;

                _currentEndurance -= Time.deltaTime;
                
                if (_currentEndurance <= 0)
                {
                    _currentEndurance = 0;

                    _isSprinting = false;
                    _sprintOnCooldown = true;
                }
            }
            else
            {
                _isSprinting = false;
            }
        }

    }
}