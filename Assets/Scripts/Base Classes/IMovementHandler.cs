
using UnityEngine;

public interface IMovementHandler
{
    Observer<IMovementValue> MovementObserver { get; }
    Vector3 Movement { get; }
}