using System;
using UnityEngine;

public class Character : MonoBehaviour, IJumper, IDiedNotifier
{
    public event Action Died;
    public event Action Jumped
    {
        add => _jumper.Jumped += value;
        remove => _jumper.Jumped -= value;
    }

    private int _points;

    private PhysicsJumper _jumper;

    [SerializeField] private float _jumpForce;
    [SerializeField] private int _moveForce;
    [SerializeField] private int _rateChangeMoveForce;

    private Vector3 _leftMoveForce;
    private Vector3 _rightMoveForce;

    [SerializeField] private int _pointsPerVerticalJump = 1;
    [SerializeField] private int _pointsPerHorizontalJump = 3;

    public int Points => _points;

    private void Awake()
    {
        _jumper = new PhysicsJumper(_jumpForce, GetComponent<Rigidbody>());
        UpdateMoveVector();
    }

    private void Update()
    {
        ProcessMovement();
    }

    private void FixedUpdate()
    {
        _jumper.FixedUpdate();
    }

    private void ProcessMovement()
    {
        if (CheckKeyJump())
        {
            _jumper.Jump(Vector3.up);

            AddPoints(_pointsPerVerticalJump);
        }
        else if (CheckKeyMoveLeft())
        {
            _jumper.Jump(_leftMoveForce);
            AddPoints(_pointsPerHorizontalJump);
            UpdateMoveVector();
        }
        else if (CheckKeyMoveRight())
        {
            _jumper.Jump(_rightMoveForce);
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

    public void Die()
    {
        gameObject.SetActive(false); // выключаем объект птички
        Died?.Invoke();
    }
}
