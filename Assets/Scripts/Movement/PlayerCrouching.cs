using NaughtyAttributes;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour, IModule
{
    [SerializeField] private float _crouchHeight;
    [SerializeField] private float _crouchTimeout;
    
    [ShowNonSerializedField] private float _controllerHeight;
    [ShowNonSerializedField] private float _controllerOffset;

    [ShowNonSerializedField] private float _crouchTimeoutDelta;

    [ShowNativeProperty] public bool IsCrouching { get; private set; }

    public Vector3 Value { get; private set; }

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
        _moduleHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.UnSubscribe(this);
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
            if (!IsCrouching && _crouchTimeoutDelta > 0)
            {
                _crouchTimeoutDelta -= Time.deltaTime;
            }

            if (_input.CrouchPressed && _crouchTimeoutDelta <= 0)
            {

                _crouchTimeoutDelta = _crouchTimeout;

                _controller.height = _crouchHeight;
                _controller.center = Vector3.up * -_controllerOffset;
            }
            
            if (!_input.CrouchPressed && IsCrouching)
            {
                _controller.height = _controllerHeight;
                _controller.center = Vector3.zero;
                
                IsCrouching = false;

            }
        }
    }

}
