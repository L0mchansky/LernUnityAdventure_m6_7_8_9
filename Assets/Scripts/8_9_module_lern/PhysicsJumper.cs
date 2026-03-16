using System;
using UnityEngine;

public class PhysicsJumper
{
    public event Action Jumped;

    private float _jumpForce;
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private bool _jumpRequested;

    public PhysicsJumper(float jumpForce, Rigidbody rigidbody)
    {
        _jumpForce = jumpForce;
        _rigidbody = rigidbody;
    }

    public void Jump(Vector3 direction)
    {
        if (_jumpRequested == false)
        {
            _direction = direction.normalized;
            _jumpRequested = true;
        }
    }

    public void FixedUpdate()
    {
        if (_jumpRequested)
        {
            _rigidbody.AddForce(_direction * _jumpForce, ForceMode.Impulse);
            _jumpRequested = false;
            Jumped?.Invoke();
        }
    }
}
