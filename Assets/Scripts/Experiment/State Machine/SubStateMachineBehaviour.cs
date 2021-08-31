using NaughtyAttributes;
using UnityEngine;

public class SubStateMachineBehaviour : State
{
#if UNITY_EDITOR
    [SerializeField] private string _currentStateName;
#endif

    [HorizontalLine(color: EColor.Gray)]
    [SerializeField] private StateInner _innerState;

    private StateMachine _stateMachine;
    private StateMachine StateMachine
    {
        get
        {
            if (_stateMachine != null)
            {
                return _stateMachine;
            }

            _stateMachine = new StateMachine(_innerState);

            return _stateMachine;
        }
    }

    private void Start()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        StateMachine.ChangeState(_innerState);
    }

    private void Update()
    {
        StateMachine.Tick();

#if UNITY_EDITOR
        if (StateMachine.CurrentState != null)
        {
            _currentStateName = StateMachine.CurrentState.ToString();
        }
#endif
    }

}
