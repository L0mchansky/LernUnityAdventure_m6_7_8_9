using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charter : MonoBehaviour
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

    [SerializeField] private float _additiveScalePerJump;
    [SerializeField] private float _maxScaleLimit;
    [SerializeField] private float _speedScale;
    private Vector3 _defaultScale;
    private Vector3 _targetScale;

    [SerializeField] private ParticleSystem _explosion;

    private string _jumpKey = "Jump";
    [SerializeField] private Animator _animator;

    private Vector3 MaxScale 
        => new Vector3(
            _defaultScale.x * _maxScaleLimit, 
            _defaultScale.y * _maxScaleLimit, 
            _defaultScale.z * _maxScaleLimit
            );

    private void Awake()
    {
        _defaultScale = _targetScale = transform.localScale;
        _rigidbody = GetComponent<Rigidbody>();
        UpdateMoveVector();
    }

    private void Update()
    {
        UpdateScale();
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (CheckKeyJump())
        {
            _animator.SetTrigger(_jumpKey);
            _rigidbody.AddForce(_jumpForce, ForceMode.Impulse);
            AddPoints(_pointsPerVerticalJump);
            IncreaseTargetScale();
        }
        else if (CheckKeyMoveLeft())
        {
            _animator.SetTrigger(_jumpKey);
            _rigidbody.AddForce(_leftMoveForce, ForceMode.Impulse);
            AddPoints(_pointsPerHorizontalJump);
            UpdateMoveVector();
            IncreaseTargetScale();
        }
        else if (CheckKeyMoveRight())
        {
            _animator.SetTrigger(_jumpKey);
            _rigidbody.AddForce(_rightMoveForce, ForceMode.Impulse);
            AddPoints(_pointsPerHorizontalJump);
            UpdateMoveVector();
            IncreaseTargetScale();
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

    private void IncreaseTargetScale()
    {
        _targetScale = new Vector3(
            _targetScale.x += _additiveScalePerJump,
            _targetScale.y += _additiveScalePerJump,
            _targetScale.z += _additiveScalePerJump
            );

        if (_targetScale.x > MaxScale.x)
            _targetScale.x = MaxScale.x;

        if (_targetScale.y > MaxScale.y)
            _targetScale.y = MaxScale.y;

        if (_targetScale.z > MaxScale.z)
            _targetScale.z = MaxScale.z;
    }

    private void UpdateScale()
    {
        if (_targetScale.x > _defaultScale.x)
            _targetScale.x -= Time.deltaTime * _speedScale;

        if (_targetScale.y > _defaultScale.y)
            _targetScale.y -= Time.deltaTime * _speedScale;

        if (_targetScale.z > _defaultScale.z)
            _targetScale.z -= Time.deltaTime * _speedScale;

        transform.localScale = _targetScale;
    }

    public void Die()
    {
        gameObject.SetActive(false); // выключаем объект птички
        _explosion.transform.position = transform.position;
        _explosion.Play();
    }
}
