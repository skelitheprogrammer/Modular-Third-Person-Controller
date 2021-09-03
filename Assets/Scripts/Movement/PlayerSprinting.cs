using UnityEngine;
using NaughtyAttributes;

public class PlayerSprinting : MonoBehaviour, IModule
{
    [Header("Sprint Settings")]
    [SerializeField] private float _sprintSpeed = 10f;

    [SerializeField] private float _endurance = 4f;
    [Range(0, 1f)]
    [SerializeField] private float _enduranceTresholdPercentage = .5f;

    [ShowNonSerializedField] private bool _isSprinting;

    [ShowNonSerializedField] private bool _sprintOnCooldown;
    [ShowNonSerializedField] private float _currentEndurance;

    private IModuleHandler _moduleHandler;
    private IMovementHandler _handler;
    private InputReader _input;

    private void Awake()
    {
        _moduleHandler = GetComponent<IModuleHandler>();
        _handler = GetComponent<IMovementHandler>();
        _input = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _moduleHandler.ModuleObserver.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.ModuleObserver.Unsubscribe(this);
    }

    public void OnUpdateModule()
    {
        Sprint();
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

        if (_handler.Movement != Vector3.zero)
        {
            if (_input.IsSprintPressed && _currentEndurance >= 0f && !_sprintOnCooldown)
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
