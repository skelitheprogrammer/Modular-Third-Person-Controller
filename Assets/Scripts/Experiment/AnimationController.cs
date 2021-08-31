using NaughtyAttributes;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [AnimatorParam(nameof(_animator))] [SerializeField] private int _forwardHash;
    [AnimatorParam(nameof(_animator))] [SerializeField] private int _sideHash;
    [AnimatorParam(nameof(_animator))] [SerializeField] private int _jumpHash;
    [AnimatorParam(nameof(_animator))] [SerializeField] private int _sprintHash;
    [AnimatorParam(nameof(_animator))] [SerializeField] private int _inAirHash;

    private PlayerMovement _movement;
    private GroundCheckerBase _groundChecker;
    private InputReader _reader;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundCheckerBase>();
        _movement = GetComponent<PlayerMovement>();
        _reader = GetComponent<InputReader>();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(_sprintHash, _reader.IsSprintPressed);

        if (_reader.IsJumpPressed)
        {
            _animator.SetTrigger(_jumpHash);
        }

        _animator.SetBool(_inAirHash, !_groundChecker.GetGrounded());
    }
}

