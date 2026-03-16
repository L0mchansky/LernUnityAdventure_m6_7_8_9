using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;



public class MagicMateria : MonoBehaviour
{
    [SerializeField] GameObject _passiveParticle;
    [SerializeField] ParticleSystem _burstParticle;
    [SerializeField] GameObject _light;
    private void OnTriggerEnter(Collider other)
    {
        AvtivationEvent();
    }

    private void AvtivationEvent()
    {
        _burstParticle.Play();
        StopEffect();
        gameObject.SetActive(false);
    }

    public void StartEffect()
    {
        _light.SetActive(true);
        _passiveParticle.SetActive(true);
    }

    public void StopEffect()
    {
        _light.SetActive(false);
        _passiveParticle.SetActive(false);
    }

    public void ReloadObject()
    {
        StopEffect();
        gameObject.SetActive(true);
    }
}
