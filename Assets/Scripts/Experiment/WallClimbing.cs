using NaughtyAttributes;
using UnityEngine;

public class WallClimbing : MonoBehaviour, IModule
{
    [SerializeField] private Vector3 topRayOffset;
    [SerializeField] private Vector3 botRayOffset;

    [SerializeField] private float _rayLength;

    [ShowNonSerializedField] private bool _topHit;
    [ShowNonSerializedField] private bool _botHit;
    [ShowNonSerializedField] private bool _ableClimb;
    [ShowNonSerializedField] private bool _isClimbing;

    private RaycastHit hit;

    private InputReader _input;

    private IModuleHandler _moduleHandler;


    private void Awake()
    {
        _input = GetComponent<InputReader>();
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

    /*    private void Update()
        {
            DetectClimb();
            Climb();
        }*/

    public void OnUpdateModule()
    {
        DetectClimb();
        Climb();
    }

    private void DetectClimb()
    {
        _topHit = Physics.Raycast(transform.position + topRayOffset, transform.forward, _rayLength);
        _botHit = Physics.Raycast(transform.position + botRayOffset, transform.forward, out hit, _rayLength);

        _ableClimb = _botHit && !_topHit;
    }

    private void Climb()
    {
        if (!_ableClimb) return;

        if (_input.ClimbPressed && !_isClimbing)
        {
            _isClimbing = true;
            Vector3 movePos = hit.point + Vector3.up + transform.forward/3f;

            transform.position = movePos;

            if (transform.position == movePos)
            {
                _isClimbing = false;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _topHit ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position + topRayOffset, transform.forward * _rayLength);
        Gizmos.color = _botHit ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position + botRayOffset, transform.forward * _rayLength);
    }


}
