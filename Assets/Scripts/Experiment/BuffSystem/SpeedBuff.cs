using UnityEngine;

[CreateAssetMenu(menuName = "Buff system/Buff logic/Create new Speed buff logic")]
public class SpeedBuff : BuffLogic
{
    [SerializeField] private float _value;

    private MovementTest _movement;

    public override void Init(GameObject go, BuffScriptable buff)
    {
        _movement = go.GetComponent<MovementTest>();
        BuffReference = buff;

        if (BuffReference.IsStackable)
        {
            _value *= BuffReference.Stacks;
        }
    }

    public override void AddEffect()
    {
        _movement.speed += _value;
    }

    public override void RemoveEffect()
    {
        _movement.speed -= _value;
    }
}