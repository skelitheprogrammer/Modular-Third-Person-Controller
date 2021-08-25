using UnityEngine;

public abstract class BuffLogic : ScriptableObject
{
    public BuffScriptable BuffReference { get; protected set; }

    public abstract void Init(GameObject go, BuffScriptable buff);
    public abstract void AddEffect();
    public abstract void RemoveEffect();
}
