using System;
using UnityEngine;

public class JumpPuffView : MonoBehaviour
{
    private readonly int JumpKey = Animator.StringToHash("Jump");

    private IJumper _jumper;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _jumper = GetComponentInParent<IJumper>();
        _jumper.Jumped += OnJumped;
    }

    private void OnDestroy()
    {
        _jumper.Jumped -= OnJumped;
    }

    private void OnJumped()
    {
        _animator.SetTrigger(JumpKey);
    }
}

