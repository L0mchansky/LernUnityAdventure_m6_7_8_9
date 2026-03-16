using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] GameObject _light;
    [SerializeField] Animator _animator;
    string _startKey = "StartAnimation";
    string _stopKey = "StopAnimation";
    public void StartAnimation()
    {
        StopEffect();
        _animator.ResetTrigger(_stopKey);
        _animator.SetTrigger(_startKey);
    }
    public void StartEffect()
    {
        _light.SetActive(true);
    }

    public void StopEffect()
    {
        _light.SetActive(false);
    }

    public void ReloadAnimation()
    {
        _animator.SetTrigger(_stopKey);
        _animator.ResetTrigger(_startKey);
        StopEffect();
    }
}
