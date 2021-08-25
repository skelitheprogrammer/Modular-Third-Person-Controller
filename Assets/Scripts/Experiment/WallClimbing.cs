using NaughtyAttributes;
using UnityEngine;

public class WallClimbing : MonoBehaviour
{
    [SerializeField] private Vector3 topRayOffset;
    [SerializeField] private Vector3 botRayOffset;
    [SerializeField] private float _rayLength;

    [ShowNonSerializedField] private bool _topHit;
    [ShowNonSerializedField] private bool _botHit;
    [ShowNonSerializedField] private bool _isClimbing;
    private InputReader _input;
    private GroundCheckerBase _groundChecker;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundCheckerBase>();
        _input = GetComponent<InputReader>();
    }

    private void Update()
    {
        DetectClimb();   
    }

    private void DetectClimb()
    {
        _topHit = Physics.Raycast(transform.position + topRayOffset, transform.forward, out var topHit, _rayLength);
        _botHit = Physics.Raycast(transform.position + botRayOffset, transform.forward, out var botHit, _rayLength);
        
        if (_input.ClimbPressed && !_groundChecker.GetGrounded())
        {
            if (_botHit && !_topHit)
            {
            }
        }
    }

    private void Climb(Vector3 to)
    {

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = _topHit ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position + topRayOffset, transform.forward * _rayLength);

        Gizmos.color = _botHit ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position + botRayOffset, transform.forward * _rayLength);
    }
}
