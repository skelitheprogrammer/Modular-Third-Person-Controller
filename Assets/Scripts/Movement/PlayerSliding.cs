using NaughtyAttributes;
using UnityEngine;

public class PlayerSliding : MonoBehaviour, IMovementValue, IModule
{
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _accelerationSpeed;

    [ShowNonSerializedField] private float _currentSlideSpeed;
    [ShowNonSerializedField] private float _targetSlideSpeed;

    [ShowNonSerializedField] private Vector3 _slideMovement;

    public Vector3 Value { get; private set; }

    private IMovementHandler _handler;
    private IModuleHandler _moduleHandler;

    private SlideCheck _slideChecker;
    private GravityBase _gravity;

    private void Awake()
    {
        _gravity = GetComponent<GravityBase>();
        _handler = GetComponent<IMovementHandler>();
        _slideChecker = GetComponent<SlideCheck>();
        _moduleHandler = GetComponent<IModuleHandler>();
    }

    private void OnEnable()
    {
        _moduleHandler.ModuleObserver.Subscribe(this);
        _handler.MovementObserver.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.ModuleObserver.Unsubscribe(this);
        _handler.MovementObserver.Unsubscribe(this);
    }

    public void OnUpdateModule()
    {
        SlopeSlide();
    }

    private void SlopeSlide()
    {
        if (_slideChecker.IsSliding)
        {
            Vector3 normal = _slideChecker.HitNormal;

            Vector3 movement = new Vector3(normal.x, -normal.y, normal.z);
            Vector3.OrthoNormalize(ref normal, ref movement);

            _slideMovement = movement;
            _targetSlideSpeed = _slideSpeed;

            _slideMovement *= _currentSlideSpeed;
            _slideMovement.y -= _gravity.Gravity * Time.deltaTime;

        }
        else
        {
            _targetSlideSpeed = 0;
            _slideMovement.y = 0;

            _slideMovement = Vector3.MoveTowards(_slideMovement, Vector3.zero, _accelerationSpeed * Time.deltaTime);
        }

        _currentSlideSpeed = Mathf.MoveTowards(_currentSlideSpeed, _targetSlideSpeed, _accelerationSpeed * Time.deltaTime);

        Value = _slideMovement;
    }


}
