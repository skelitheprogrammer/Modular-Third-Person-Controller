using UnityEngine;

public class StateMachineBehaviour : MonoBehaviour
{
    public string currentStateName;

    [SerializeField] private State _initState;

    private StateMachine _stateMachine;
    private StateMachine StateMachine
    {
        get
        {
            if (_stateMachine != null) 
            { 
                return _stateMachine; 
            }

            _stateMachine = new StateMachine(_initState);
            
            return _stateMachine;
        }
    }

    private void Update()
    {
        StateMachine.Tick();
        currentStateName = StateMachine.CurrentState.ToString();
    }
}
