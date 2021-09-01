using UnityEngine;

public interface IMovementHandler
{
    void Subscribe(IMovementValue value);
    void UnSubscribe(IMovementValue value);
    Vector3 Movement { get; }
}