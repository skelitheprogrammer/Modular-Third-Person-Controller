using UnityEngine;

public class StateMachineBehaviour : MonoBehaviour
{
    [SerializeField] private State _initState;

    public StateMachine StateMachine { get; private set; }

    public string currentStateName;

    private void Awake()
    {
        StateMachine = new StateMachine(_initState);
    }

    private void OnEnable()
    {
        StateMachine.ChangeState(_initState);
    }

    private void Update()
    {
        StateMachine.Tick();
        currentStateName = StateMachine.CurrentState.name;
    }
}
