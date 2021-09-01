using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerHandler : MonoBehaviour, IMovementHandler, IModuleHandler
{
    public CharacterController Controller { get; private set; }

    private readonly List<IModule> _modules = new List<IModule>();
    private readonly List<IMovementValue> _modifiers = new List<IMovementValue>();

    private Vector3 _movement;
    [ShowNativeProperty] public Vector3 Movement => _movement;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateModules();
        Move();
    }

    #region Subscriptions
    public void Subscribe(IMovementValue modifier)
    {
        _modifiers.Add(modifier);
    }

    public void UnSubscribe(IMovementValue modifier)
    {
       _modifiers.Remove(modifier);
    }

    public void Subscribe(IModule module)
    {
        _modules.Add(module);
    }

    public void UnSubscribe(IModule module)
    {
        _modules.Remove(module);
    }
    #endregion
    private void UpdateModules()
    {
        foreach (var item in _modules)
        {
            item.OnUpdateModule();
        }
    }

    private void Move()
    {
        _movement = Vector3.zero;
        
        foreach(IMovementValue item in _modifiers)
        {
            _movement += item.Value;
        }

        Controller.Move(_movement * Time.deltaTime);
    }
}
