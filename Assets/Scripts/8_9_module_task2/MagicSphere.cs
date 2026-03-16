using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    [SerializeField] GameObject _light;
    [SerializeField] GameObject _particle;
    [SerializeField] Animator _animator;
    string _createKey = "Create";
    string _deleteKey = "Delete";
    public void StartAnimation()
    {
        _animator.ResetTrigger(_deleteKey);
        _animator.SetTrigger(_createKey);
        _light.SetActive(true);
        _particle.SetActive(true);
    }

    public void ReloadAnimation()
    {
        _animator.ResetTrigger(_createKey);
        _animator.SetTrigger(_deleteKey);   
        _light.SetActive(false);
        _particle.SetActive(false);
    }
}
