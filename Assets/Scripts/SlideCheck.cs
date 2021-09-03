using NaughtyAttributes;
using UnityEngine;

public class SlideCheck : MonoBehaviour, IModule
{
    [SerializeField] private float _slopeLimit = 45f;

    [ShowNativeProperty] public Vector3 HitNormal { get; private set; }
    [ShowNativeProperty] public bool IsSliding { get; private set; }
   
    private IModuleHandler _moduleHandler;

    private void Awake()
    {
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
    public void OnUpdateModule()
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
