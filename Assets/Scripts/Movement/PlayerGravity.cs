using NaughtyAttributes;
using UnityEngine;

public class PlayerGravity : GravityBase, IMovementValue
{
    [SerializeField] private float _groundGravity = -7;
    public float GroundGravity => _groundGravity;

    [SerializeField] private float _gravity = -20f;
    public override float Gravity => _gravity;

    [SerializeField] private float _maxFallSpeed;

    private float _currentGravityVelocity;

    private bool _isLanded;

    private IMovementHandler _handler;
    private GroundCheckerBase _groundCheck;

    [ShowNativeProperty] public Vector3 Value { get; private set; }

    private void Awake()
    {
        _handler = GetComponent<IMovementHandler>();
        _groundCheck = GetComponent<GroundCheckerBase>();
    }

    private void OnEnable()
    {
        _handler.Subscribe(this);
    }

    private void OnDisable()
    {
        _handler.UnSubscribe(this);
    }

    private void Update()
    {
        ApplyGravity();
    }

    protected override void ApplyGravity()
    {
        if (_groundCheck.GetGrounded())
        {
            if (!_isLanded)
            {
                _isLanded = true;
                _currentGravityVelocity = -_groundGravity;
            }
        }
        else
        {
            if (_isLanded)
            {
                _currentGravityVelocity = -.5f;
                _isLanded = false;
            }

            _currentGravityVelocity = Mathf.MoveTowards(_currentGravityVelocity, -_maxFallSpeed, _gravity * Time.deltaTime);
        }

        Value = Vector3.up * _currentGravityVelocity;
    }

}
