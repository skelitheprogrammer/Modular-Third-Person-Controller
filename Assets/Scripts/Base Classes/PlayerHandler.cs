using NaughtyAttributes;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerHandler : MonoBehaviour, IMovementHandler, IModuleHandler
{
    public CharacterController Controller { get; private set; }

    public Observer<IMovementValue> MovementObserver { get; private set; } = new Observer<IMovementValue>();
    public Observer<IModule> ModuleObserver { get; private set; } = new Observer<IModule>();

    private Vector3 _movement;
    [ShowNativeProperty] public Vector3 Movement => _movement;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateModules();
        MovePlayer();
    }
    private void UpdateModules()
    {

        foreach (var item in ModuleObserver.subjects)
        {
            item.OnUpdateModule();
        }
    }

    private void MovePlayer()
    {
        _movement = Vector3.zero;

        foreach(IMovementValue item in MovementObserver.subjects)
        {
            _movement += item.Value;
        }

        Controller.Move(_movement * Time.deltaTime);
    }
}