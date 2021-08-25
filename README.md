# Modular-Third-Person-Controller
With this third person controller you could make new modules for YOUR character controller much easier and faster!

## About
This repository was created for the purpose of evaluating my way of creating a controller. The main idea was taken from the [Dapper Dino](https://www.youtube.com/watch?v=-PCvfltKguE), but I took only the concept. I tried to use SOLID principles to make project stable and expandable.

**!You can find experemental things in this repo, which can be removed/changed!**

### Features

- Character Controller based.
- NEW Input System.
- Input reader script

- Gravity
- Jump
- Walk/Sprint
- Camera rotation
- Player rotation
- Sliding
- Crouching

# How this controller works
Controller based on Character Controller Component and Movement Handler implementation.

![image](https://user-images.githubusercontent.com/47909066/130787089-52e42ddb-5c69-4c1a-ab06-862da21e055e.png)

**MovementHandler** inherits **IMovementHandler** interface to let you Subscribe-Unsubscrive different movement modules.

![image](https://user-images.githubusercontent.com/47909066/130787459-25b412a0-691e-47df-9f5e-576d07a06aec.png)

![image](https://user-images.githubusercontent.com/47909066/130787732-503071ec-0982-4a6c-a3f0-ff5635578c67.png)

![image](https://user-images.githubusercontent.com/47909066/130787595-7884aa3a-83bc-45a7-9f87-6ac08389700c.png)

To make **MovementHandler** work with your module, you need to make sure that the module has a **IMovementValue** interface, **IMovementHandler** reference.

Example:

```csharp
public class PlayerMovement : MonoBehaviour, IMovementValue
{
    private IMovementHandler _handler;
    public Vector3 Value { get; private set; }
    
    private void Awake()
    {
        _handler = GetComponent<IMovementHandler>();
    }

    private void OnEnable()
    {
        _handler.Subscribe(this);
    }
    
    private void OnDisable()
    {
        _handler.UnSubscribe(this);
    }
    
    private void Update()
    {
       MoveForward(Vector3.forward)
    }
    
    private void Move(Vector3 direction)
    {
        Value = direction * 3f;
    }
}
```

