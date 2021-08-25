using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MovementHandler : MonoBehaviour, IMovementHandler
{
    public CharacterController Controller { get; private set; }

    private readonly List<IMovementValue> _modifiers = new List<IMovementValue>();

    [ShowNativeProperty] public Vector3 Movement { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    public void Subscribe(IMovementValue modifier)
    {
        _modifiers.Add(modifier);
    }

    public void UnSubscribe(IMovementValue modifier)
    {
        _modifiers.Remove(modifier);
    }

    private void Move()
    {
        Movement = Vector3.zero;
        
        foreach(IMovementValue item in _modifiers)
        {
            Movement += item.Value;
        }

        Controller.Move(Movement * Time.deltaTime);
    }

}
