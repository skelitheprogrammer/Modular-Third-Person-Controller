using UnityEngine;

public abstract class GroundCheckerBase : MonoBehaviour
{

    public abstract bool GetGrounded();
    protected abstract void Check();
}