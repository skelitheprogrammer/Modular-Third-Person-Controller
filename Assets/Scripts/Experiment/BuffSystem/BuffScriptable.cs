using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff system/Create new Buff", fileName = "New Buff holder")]
public class BuffScriptable : ScriptableObject
{
    [SerializeField] private BuffLogic _buffLogic;
    public BuffLogic BuffLogic => _buffLogic;

    [SerializeField] private bool _isStackable;
    public bool IsStackable => _isStackable; 

    [EnableIf(nameof(_isStackable))]
    [SerializeField] private float _stacks;
    public float Stacks => _stacks;

    public virtual BuffScriptable ConstructBuff(GameObject go)
    {
        BuffScriptable buff = Instantiate(this);
        buff._buffLogic.Init(go,buff);

        return buff;
    }
}
