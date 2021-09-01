using NaughtyAttributes;
using UnityEngine;

public class PlayerGravity : GravityBase, IMovementValue, IModule
{
    [SerializeField] private float _groundGravity = -7;
    public float GroundGravity => _groundGravity;

    [SerializeField] private float _gravity = -20f;
    public override float Gravity => _gravity;

    [SerializeField] private float _maxFallSpeed;

    private float _currentGravityVelocity;

    private bool _isLanded;

    private IMovementHandler _handler;
    private IModuleHandler _moduleHandler;
    private GroundCheckerBase _groundCheck;

    [ShowNativeProperty] public Vector3 Value { get; private set; }

    private void Awake()
    {
        _handler = GetComponent<IMovementHandler>();
        _moduleHandler = GetComponent<IModuleHandler>();
        _groundCheck = GetComponent<GroundCheckerBase>();
    }

    private void OnEnable()
    {
        _moduleHandler.Subscribe(this);
        _handler.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.UnSubscribe(this);
        _handler.UnSubscribe(this);
    }

    public void OnUpdateModule()
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
