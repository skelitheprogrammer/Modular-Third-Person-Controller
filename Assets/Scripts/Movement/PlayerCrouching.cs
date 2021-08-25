using NaughtyAttributes;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour
{

    [SerializeField] private float _crouchHeight;
    [SerializeField] private float _crouchTimeout;

    [ShowNonSerializedField] private float _controllerHeight;
    [ShowNonSerializedField] private float _controllerOffset;

    [ShowNonSerializedField] private float _crouchTimeoutDelta;

    [ShowNativeProperty] public bool IsCrouching { get; private set; }

    private CharacterController _controller;
    private InputReader _input;
    private GroundCheckerBase _groundChecker;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputReader>();
        _groundChecker = GetComponent<GroundCheckerBase>();
    }

    private void Start()
    {
        _controllerHeight = _controller.height;
        _controllerOffset = (_controller.height - _crouchHeight) / 2;
    }

    private void Update()
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

                IsCrouching = true;
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
