using NaughtyAttributes;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour, IModule, IChangeableSpeed
{
    [SerializeField] private float _crouchSpeed;
    public float Speed => _crouchSpeed;

    [SerializeField] private float _crouchHeight;
    [SerializeField] private float _crouchTimeout;
    
    [ShowNonSerializedField] private float _controllerHeight;
    [ShowNonSerializedField] private float _controllerOffset;

    [ShowNonSerializedField] private float _crouchTimeoutDelta;

    [ShowNonSerializedField] private bool _isCrouching;
    public bool IsChanging => _isCrouching;

    private CharacterController _controller;
    private InputReader _input;
    private GroundCheckerBase _groundChecker;
    private IModuleHandler _moduleHandler;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputReader>();
        _groundChecker = GetComponent<GroundCheckerBase>();
        _moduleHandler = GetComponent<IModuleHandler>();
    }

    private void OnEnable()
    {
        _moduleHandler.ModuleObserver.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.ModuleObserver.Unsubscribe(this);
    }

    private void Start()
    {
        _controllerHeight = _controller.height;
        _controllerOffset = (_controller.height - _crouchHeight) / 2;
    }

    public void OnUpdateModule()
    {
        Crouch();
    }

    private void Crouch()
    {
        if (_groundChecker.GetGrounded())
        {
            if (!_isCrouching && _crouchTimeoutDelta > 0)
            {
                _crouchTimeoutDelta -= Time.deltaTime;
            }

            if (_input.CrouchPressed && _crouchTimeoutDelta <= 0)
            {

                _crouchTimeoutDelta = _crouchTimeout;

                _controller.height = _crouchHeight;
                _controller.center = Vector3.up * -_controllerOffset;

                _isCrouching = true;

            }
            
            if (!_input.CrouchPressed && _isCrouching)
            {
                _controller.height = _controllerHeight;
                _controller.center = Vector3.zero;
                
                _isCrouching = false;

            }
        }
    }

}