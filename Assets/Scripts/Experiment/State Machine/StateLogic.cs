using UnityEngine;
using UnityEngine.Events;

public abstract class StateLogic : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Exit();
}
