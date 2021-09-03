﻿using NaughtyAttributes;
using UnityEngine;

public class GroundCheckSphereBased : GroundCheckerBase, IModule
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;
    [SerializeField] private float _checkRadius;
    
    [ShowNonSerializedField] private bool _isGrounded;

    private CharacterController _controller;

    private IModuleHandler _moduleHandler;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _moduleHandler = GetComponent<IModuleHandler>();
    }

    private void OnEnable()
    {
        _moduleHandler.ModuleObserver.Subscribe(this);
    }

    private void OnDisable()
    {
        _moduleHandler.ModuleObserver.Unsubscribe(this);
    }

    public void OnUpdateModule()
    {
        Check();
    }

    protected override void Check()
    {
        Vector3 finalPosition = _target.position + _offset - Vector3.up * _controller.height / 2 + _controller.center;
        _isGrounded = Physics.CheckSphere(finalPosition, _checkRadius);
    }

    public override bool GetGrounded()
    {
        return _isGrounded;
    }

    private void OnDrawGizmos()
    {
        if (_controller == null) return;

        Gizmos.color = _isGrounded ? Color.red : Color.green;
        Gizmos.DrawSphere(_target.position + _offset - Vector3.up * _controller.height / 2 + _controller.center, _checkRadius);
    }


}
