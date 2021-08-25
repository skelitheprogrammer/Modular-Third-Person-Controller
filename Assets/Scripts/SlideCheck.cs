using NaughtyAttributes;
using UnityEngine;

public class SlideCheck : MonoBehaviour
{
    [SerializeField] private float _slopeLimit = 45f;

    [ShowNativeProperty] public Vector3 HitNormal { get; private set; }
    [ShowNativeProperty] public bool IsSliding { get; private set; }

    private void Update()
    {
        Check();    
    }

    private void Check()
    {
        float angle = Vector3.Angle(Vector3.up, HitNormal);

        IsSliding = angle > _slopeLimit;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HitNormal = hit.controller.collisionFlags == CollisionFlags.Below ? hit.normal : Vector3.zero;
    }
}
