using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform _origin;

    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    private float _rotationVelocity;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _origin.eulerAngles.y, ref _rotationVelocity, RotationSmoothTime);

        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
}
