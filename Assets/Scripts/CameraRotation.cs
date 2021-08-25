using UnityEngine;
using NaughtyAttributes;

public class CameraRotation : MonoBehaviour
{
    private InputReader _input;

    [SerializeField] private Transform _cameraRoot;

    [Space(5f)]
    [SerializeField] private float _topClamp = 80f;
    [SerializeField] private float _bottomClamp = -80f;
    
    [Space(5f)]
    [SerializeField] private float _sensitivityX = 1f;
    [SerializeField] private float _sensitivityY = 1f;

    [ShowNonSerializedField] private Vector2 _rotation;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
    }

    private void LateUpdate()
    {
        CameraRotate();
    }

    public void CameraRotate()
    {
        _rotation.x += _input.RotateInput.x * _sensitivityX;
        _rotation.y += _input.RotateInput.y * _sensitivityY;
        _rotation.y = Mathf.Clamp(_rotation.y, _bottomClamp, _topClamp);

        _cameraRoot.rotation = Quaternion.Euler(-_rotation.y, _rotation.x, 0);
    }
}
