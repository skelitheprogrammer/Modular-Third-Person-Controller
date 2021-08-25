using NaughtyAttributes;
using UnityEngine;

public class PlayerSliding : MonoBehaviour, IMovementValue
{
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _pullGravity;

    [ShowNonSerializedField] private float _currentSlideSpeed;
    [ShowNonSerializedField] private float _targetSlideSpeed;

    [ShowNonSerializedField] private Vector3 _slideMovement;

    public Vector3 Value { get; private set; }

    private IMovementHandler _handler;
    private SlideCheck _slideChecker;

    private void Awake()
    {
        _handler = GetComponent<IMovementHandler>();
        _slideChecker = GetComponent<SlideCheck>();
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
        SlopeSlide();
    }

    private void SlopeSlide()
    {
        Vector3 normal = _slideChecker.HitNormal;

        Vector3 movement = new Vector3(normal.x, -normal.y, normal.z);
        Vector3.OrthoNormalize(ref normal, ref movement);

        if (_slideChecker.IsSliding)
        {
            _slideMovement = movement;
            _targetSlideSpeed = _slideSpeed;

            _slideMovement *= _currentSlideSpeed;
            _slideMovement.y -= _pullGravity;
        }
        else
        {
            _targetSlideSpeed = 0;
            _slideMovement.y = 0;

            _slideMovement = Vector3.MoveTowards(_slideMovement, Vector3.zero, _accelerationSpeed * Time.deltaTime);
        }

        _currentSlideSpeed = Mathf.MoveTowards(_currentSlideSpeed, _targetSlideSpeed, _accelerationSpeed * Time.deltaTime);

        Vector3 final = _slideMovement;

        Value = final;
    }
}
