using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bird : MonoBehaviour
{
    private int _points;
    private Rigidbody _rigidbody;

    [SerializeField] private Vector3 _jumpForce;
    [SerializeField] private int _moveForce;
    [SerializeField] private int _rateChangeMoveForce;

    private Vector3 _leftMoveForce;
    private Vector3 _rightMoveForce;

    [SerializeField] private int _pointsPerVerticalJump = 1;
    [SerializeField] private int _pointsPerHorizontalJump = 3;

    public int Points => _points;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        UpdateMoveVector();
    }

    private void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (CheckKeyJump())
        {
            _rigidbody.AddForce(_jumpForce, ForceMode.Impulse);
            AddPoints(_pointsPerVerticalJump);
        }
        else if (CheckKeyMoveLeft())
        {
            _rigidbody.AddForce(_leftMoveForce, ForceMode.Impulse);
            AddPoints(_pointsPerHorizontalJump);
            UpdateMoveVector();
        }
        else if (CheckKeyMoveRight())
        {
            _rigidbody.AddForce(_rightMoveForce, ForceMode.Impulse);
            AddPoints(_pointsPerHorizontalJump);
            UpdateMoveVector();
        }
    }

    private bool CheckKeyJump()
    {
        return Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool CheckKeyMoveLeft()
    {
        return Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    private bool CheckKeyMoveRight()
    {
        return Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.RightArrow);
    }

    private void UpdateMoveVector()
    {
        System.Random random = new System.Random();

        int leftChangeForce = random.Next(-_rateChangeMoveForce, _rateChangeMoveForce + 1);
        int rightChangeForce = random.Next(-_rateChangeMoveForce, _rateChangeMoveForce + 1);

        _leftMoveForce = new Vector3(-_moveForce + leftChangeForce, 0, 0);
        _rightMoveForce = new Vector3(_moveForce + rightChangeForce, 0, 0);
    }

    private void AddPoints(int value)
    {
        _points += value;
    }

    public void ResetPoints()
        => _points = 0;
}