using UnityEngine;

public class JumpScaleChangerView : MonoBehaviour
{
    [SerializeField] private float _additiveScalePerJump = 0.25f;
    [SerializeField] private float _maxScaleLimit;
    [SerializeField] private float _speedScale;

    private IJumper _jumper;

    private Vector3 _defaultScale;
    private Vector3 _targetScale;

    private Vector3 MaxScale
        => new Vector3(
            _defaultScale.x * _maxScaleLimit,
            _defaultScale.y * _maxScaleLimit,
            _defaultScale.z * _maxScaleLimit
            );

    private void Start()
    {
        _jumper = GetComponentInParent<IJumper>();
        _defaultScale = _targetScale = transform.localScale;
        _jumper.Jumped += OnJumped;
    }

    private void Update()
    {
        UpdateScale();
    }

    private void OnDestroy()
    {
        _jumper.Jumped -= OnJumped;
    }

    private void OnJumped()
    {
        IncreaseTargetScale();
    }

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
}