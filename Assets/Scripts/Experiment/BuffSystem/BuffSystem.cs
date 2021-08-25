using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public List<BuffScriptable> buffs = new List<BuffScriptable>();
    public List<TimeBuff> timeBuffs = new List<TimeBuff>();
    
    public BuffScriptable speedBuff;
    public BuffScriptable timedSpeedBuff;

    private void Update()
    {
        UpdateTimedBuffs();
    }

    public void AddBuff(BuffScriptable buff)
    {
        buff = buff.ConstructBuff(gameObject);
        
        if (buff is TimeBuff)
        {
            timeBuffs.Add((TimeBuff)buff);
        }

        buffs.Add(buff);
        buff.BuffLogic.AddEffect();
    }

    public void RemoveBuff(BuffScriptable buff)
    {
        buff.BuffLogic.RemoveEffect();
        
        if (buff is TimeBuff)
        {
            timeBuffs.Remove((TimeBuff)buff);
        }
        
        buffs.Remove(buff);
    }

    private void UpdateTimedBuffs()
    {
        try
        {
            foreach (TimeBuff item in timeBuffs)
            {
                item.Tick(Time.deltaTime);

                if (item.IsFinished)
                {
                    RemoveBuff(item);
                }
            }

        }
        catch
        {
            return;
        }

    }
}
