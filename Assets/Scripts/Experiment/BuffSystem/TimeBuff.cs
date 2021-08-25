using UnityEngine;

[CreateAssetMenu(menuName = "Buff system/Create new Time Buff")]
public class TimeBuff : BuffScriptable
{
    [SerializeField] private float _duration;
    
    public bool IsFinished { get; private set; }

    public void Tick(float deltatime)
    {
        _duration -= deltatime;
        
        if (_duration < 0) 
        {
            IsFinished = true;
        }
    }
}
