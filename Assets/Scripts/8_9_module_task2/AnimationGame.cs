using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGame : MonoBehaviour
{
    [SerializeField] Pot _pot;
    [SerializeField] Torch[] _torches;
    [SerializeField] MagicSphere _magicSphere;
    [SerializeField] float _invokeCallMagicSphere;
    [SerializeField] MagicMateria _magicMateria;
    [SerializeField] Candle _candle;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopAnimations();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StartEffects();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartAnimations();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopAnimations();
        }
    }

    private void StartEffects()
    {
        _magicMateria.StartEffect();
        _pot.StartEffect();
        _candle.StartEffect();
        foreach (Torch torch in _torches)
        {
            torch.StartEffect();
        }
    }

    private void StartAnimations()
    {
        Console.WriteLine("Старт анимации!");
        _pot.StartAnimation();
        Invoke("AfterExplosion", _invokeCallMagicSphere);
    }

    private void AfterExplosion()
    {
        _magicSphere.StartAnimation();
        foreach (Torch torch in _torches)
        {
            torch.StopEffect();
        }
        _candle.StartAnimation();
    }

    private void StopAnimations()
    {
        Console.WriteLine("Стоп анимации!");
        _pot.StopAnimation();
        _magicMateria.ReloadObject();
        _magicSphere.ReloadAnimation();
        _candle.ReloadAnimation();
        foreach (Torch torch in _torches)
        {
            torch.StopEffect();
        }
    }
}
