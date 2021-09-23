using UnityEngine;

public abstract class SlopeCheckBase : MonoBehaviour
{
    public abstract float Angle { get; protected set; }
    public abstract bool IsSlopeSteep { get; protected set; }

    public abstract RaycastHit SlopeHit { get; }

    protected abstract void Check();
}