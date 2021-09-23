using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private GroundCheckerBase _groundCheck;
    [SerializeField] private InputReader _input;
    [SerializeField] private PlayerMovement _movement;

    [SerializeField] private float _smoothSpeed;
    public float _smoothValue;
    public float _test;

    private int _forwardHash;
    private int _jumpHash;
    private int _groundHash;

    private void Awake()
    {
        AssignIDs();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void AssignIDs()
    {
        _forwardHash = Animator.StringToHash("Forward");
        _jumpHash = Animator.StringToHash("IsJumping");
        _groundHash = Animator.StringToHash("IsGrounded");
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(_groundHash, _groundCheck.IsGrounded);
        _animator.SetBool(_jumpHash, _input.IsJumpPressed);

        if (_movement.CurrentSpeed > 0)
        {
            _test = _movement.CurrentSpeed / _movement.TargetSpeed;
        }
        else
        {
            _test = 0;
        }

        _smoothValue = Mathf.MoveTowards(_smoothValue, _test, _smoothSpeed * Time.deltaTime);
        _animator.SetFloat(_forwardHash, _smoothValue);
    }
}
