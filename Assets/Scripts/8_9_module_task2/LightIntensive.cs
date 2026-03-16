using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensive : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _minIntensity;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private float _timeOscillation;

    private float _timer;


    void Update()
    {
        _timer += Time.deltaTime;

        float pingPongValue = Mathf.PingPong(_timer / _timeOscillation, _timeOscillation);

        _light.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, pingPongValue);
    }
}