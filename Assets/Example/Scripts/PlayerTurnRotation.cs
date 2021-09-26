using UnityEngine;

public class PlayerTurnRotation : MonoBehaviour
{
    [SerializeField] private Transform _origin;

    [Min(0)]
    [SerializeField] private float _rotationSmoothTime = 0.01f;

    private float _rotationVelocity;

    [SerializeField] private InputReader _input;

    private void Update()
    {
        Turn();
    }

    private void Turn()
    {
        if (_input.MoveInput == Vector2.zero) return;

        var rotation = Mathf.Atan2(_input.MoveInput.x, _input.MoveInput.y) * Mathf.Rad2Deg + _origin.eulerAngles.y;
        var smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref _rotationVelocity, _rotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, smoothRotation, 0.0f);
    }
}
