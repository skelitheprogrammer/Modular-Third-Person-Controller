using UnityEngine;

public class GroundCheckSphereBased : GroundCheckerBase
{
    [SerializeField] private CharacterController _controller;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _checkRadius;

    private void Update()
    {
        Check();
    }

    protected override void Check()
    {
        WasGroundedLastframe = IsGrounded;
        IsGrounded = Physics.CheckSphere(transform.position + _offset, _checkRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.red : Color.green;
        Gizmos.DrawSphere(transform.position + _offset, _checkRadius);
    }
}
