using UnityEngine;

public class SlopeCheckBasic : SlopeCheckBase
{
    [SerializeField] private float _maxSlopeAngle = 45f;
    
    [SerializeField] private Vector3 _rayOffset = new Vector3(0,-.72f,0);
    [SerializeField] private float _rayLength = 1f;

    public override float Angle { get; protected set; }
    public override bool IsSlopeSteep { get; protected set; }

    private RaycastHit _hit;
    public override RaycastHit SlopeHit { get => _hit; }

    [SerializeField] private GroundCheckerBase _groundCheck;


    private void Update()
    {
        Check();        
    }

    protected override void Check()
    {
        if (!_groundCheck.IsGrounded) return;

        Ray raycast = new Ray(transform.position + _rayOffset, Vector3.down);

        if (Physics.Raycast(raycast, out _hit,  _rayLength))
        {
            Angle = Vector3.Angle(Vector3.up, _hit.normal);

            if (Angle > _maxSlopeAngle)
            {
                IsSlopeSteep = true;
            }
            else
            {
                IsSlopeSteep = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsSlopeSteep ? Color.red : Color.green;
        Gizmos.DrawRay(transform.position + _rayOffset, Vector3.down * _rayLength);
    }

}