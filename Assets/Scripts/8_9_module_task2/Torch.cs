using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject _light;
    [SerializeField] GameObject _particle;

    public void StartEffect()
    {
        _light.SetActive(true);
        _particle.SetActive(true);
    }

    public void StopEffect()
    {
        _light.SetActive(false);
        _particle.SetActive(false);
    }
}