using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] GameObject _light;
    [SerializeField] GameObject _particle;
    [SerializeField] Animator _animator;
    private string _flyPotKey = "StartFlyPot";
    private string _StopKey = "Stop";
    public void StartAnimation()
    {
        StartEffect();
        _animator.ResetTrigger(_StopKey);
        _animator.SetTrigger(_flyPotKey);
    }
    public void StartEffect()
    {
        _light.SetActive(true);
        _particle.SetActive(true);
    }

    public void StopAnimation()
    {
        _animator.ResetTrigger(_flyPotKey);
        _animator.SetTrigger(_StopKey);
        _light.SetActive(false);
        _particle.SetActive(false);
    }
}
