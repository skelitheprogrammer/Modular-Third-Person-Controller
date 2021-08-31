using UnityEngine;

public class SubStateMachineLogic : StateLogic
{
    [SerializeField] private SubStateMachineBehaviour _behaviour;
    
    public override void Enter()
    {
        _behaviour.enabled = true;
    }

    public override void Exit()
    {
        _behaviour.enabled = false;
    }
}
