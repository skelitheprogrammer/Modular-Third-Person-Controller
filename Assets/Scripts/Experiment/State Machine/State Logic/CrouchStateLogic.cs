using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchStateLogic : StateLogic
{
    public override void Enter()
    {
        Debug.Log("yay");
    }

    public override void Exit()
    {
        Debug.Log(":(");
    }
}
